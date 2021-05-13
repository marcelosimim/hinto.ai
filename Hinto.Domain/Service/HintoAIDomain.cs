using Hinto.Common.Enum;
using Hinto.Domain.Contract;
using Hinto.Domain.VO;
using Hinto.Entity;
using Hinto.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hinto.Domain.Service
{
    public class HintoAIDomain : IHintoAIDomain
    {
        private HintoContext _dbContext;
        public HintoAIDomain()
        {
            _dbContext = new HintoContext();
        }


        public List<MidiaVO> Recommendations(long idUsuario)
        {
            var usuario = _dbContext.Usuarios.SingleOrDefault(x => x.Id == idUsuario);

            if (usuario == null)
                throw new Exception("Usuário não encontrado.");


            return IAProcessRecommend(usuario);
        }

        private List<MidiaVO> IAProcessRecommend(Usuario usuario)
        {
            //QUERY FINAL - buscar lista de interesses do usuario
            var interessesUsuarioQuery = _dbContext.ListaInteresses.Where(x => x.UsuarioId == usuario.Id).ToList();
            var listaInteresseMidiaQuery = _dbContext.ListaInteresseMidias.ToList();


            var interessesUsuario = interessesUsuarioQuery.ToList();
            var listaInteresseMidia = listaInteresseMidiaQuery.ToList();


            var midiasInteressadas = listaInteresseMidia.ToList().Where(x => interessesUsuario.Any(c => c.Id == x.ListaInteresseId)).Select(x => x.MidiasId).ToList();

            //Se ele nao adicionou nada a lista, recomenda os tops
            if (midiasInteressadas.Count() == 0)
            {
                return GetTopMidias();
            }


            //Pega os generos favoritos dele
            //QUERY FINAL - Buscar MidiasGeneros nas midias interessadas
            var generosFavoritosQuery = _dbContext.MidiaGeneros
                                .Where(x => midiasInteressadas
                                    .Any(c => c == x.MidiaId))
                                .GroupBy(x => x.GenerosId)
                                .OrderByDescending(x => x.Count())
                                .Take(10)
                                .Select(x => x.Key);

            var generosFavoritos = generosFavoritosQuery.ToList();

            var produtoresFavoritosQuery = _dbContext.MidiaProdutores
                                .Where(x => midiasInteressadas
                                            .Any(c => c == x.MidiaId))
                                .GroupBy(x => x.ProdutoresId)
                                .OrderByDescending(x => x.Count())
                                .Take(10)
                                .Select(x => x.Key);

            var produtoresFavoritos = produtoresFavoritosQuery.ToList();

            //Aqui vai ser PUNK, dado uma lista de ids de generos, buscar filmes que tem as maiores similaridades de genero.

            var recomendacoes = BuscarMidiasGenerosSimilares(generosFavoritos, midiasInteressadas, produtoresFavoritos);

            return recomendacoes;

        }

        private List<MidiaVO> BuscarMidiasGenerosSimilares(List<long> generosFavoritos, List<long> midiasIgnoradas, List<long> produtoresFavoritos)
        {

            var listaMidiasPontos = new List<MidiaSimilarVO>();

            
            //QUERY - Pega todos que exceto os que ele ja viu.
            var listaMidiasQuery = _dbContext.Midia.Where(x => !midiasIgnoradas.Contains(x.Id));

            var listaMidiaGenerosQuery = _dbContext.MidiaGeneros.Include(x => x.Generos);

            var listaProdutoresQUery = _dbContext.MidiaProdutores.Include(x => x.Produtores);

            var listaMidias = listaMidiasQuery.ToList();

            var listaMidiaGeneros = listaMidiaGenerosQuery.ToList();

            var listaProdutores = listaProdutoresQUery.ToList();

            var listaDeMidiasGenerosBanco = listaMidias.Select(x => new MidiaGeneroProjectionVO{ 
                MidiaId = x.Id,
                GenerosId = listaMidiaGeneros.Where(c => c.MidiaId == x.Id).Select(c => c.GenerosId).ToList()
            });

            var listaDeMidiaProdutoresBanco = listaMidias.Select(x => new MidiaProdutoreProjectionVO
            {
                MidiaId = x.Id,
                Produtores = listaProdutores.Where(c => c.MidiaId == x.Id).Select(c => c.ProdutoresId).ToList()
            });

            foreach (var midiaGeneroBD in listaDeMidiasGenerosBanco) {
                var baseFavoritos =  generosFavoritos.Count();

                var quantidadeSimilaridade = generosFavoritos.Count(x => midiaGeneroBD.GenerosId.Contains(x));

                var calculoResultante = quantidadeSimilaridade * 100 / (double)baseFavoritos;

                listaMidiasPontos.Add(new MidiaSimilarVO { 
                    MidiaId = midiaGeneroBD.MidiaId,
                    Similarity = calculoResultante,
                    RecomendationType = RecomendationType.Genero
                });
            }

            foreach (var midiaGeneroBD in listaDeMidiaProdutoresBanco)
            {
                var baseFavoritos = produtoresFavoritos.Count();

                var quantidadeSimilaridade = produtoresFavoritos.Count(x => midiaGeneroBD.Produtores.Contains(x));

                var calculoResultante = quantidadeSimilaridade * 100 / (double)baseFavoritos;

                listaMidiasPontos.Add(new MidiaSimilarVO
                {
                    MidiaId = midiaGeneroBD.MidiaId,
                    Similarity = calculoResultante,
                    RecomendationType = RecomendationType.Produtor
                });
            }

            var filtradoOrdenado = listaMidiasPontos.OrderByDescending(x => x.Similarity).Where(x => x.Similarity > 40).Select(x => x.MidiaId).ToList();


            var midiasVo = _dbContext.Midia.ToList().Where(x => filtradoOrdenado.Contains(x.Id)).Select(x => new MidiaVO { 
                Id = x.Id,
                Afinidade = x.Afinidade,
                DataLancamento = x.DataLancamento,
                ImagemURL = x.ImagemUrl,
                Tipo = x.Tipo,
                Titulo = x.Titulo,
                Sinopse = x.Sinopse,
                Generos = listaMidiaGeneros.Where(c => c.MidiaId == x.Id).Select(v => new GeneroVO { 
                    Id = v.Generos.Id ,
                    Descricao = v.Generos.Descricao
                }).ToList(),
                Produtores = listaProdutores.Where(c => c.MidiaId == x.Id).Select(v => new ProdutorVO
                {
                    Id = v.Produtores.Id,
                    Nome = v.Produtores.Nome
                }).ToList()
            });

            return midiasVo.ToList();
        }

        private List<MidiaVO> GetTopMidias(int amount = 10) {
            var listaInteresses = _dbContext.ListaInteresseMidias;


            //QUERY FINAL - Busca midias top geral
            var queryTopMidias = listaInteresses.GroupBy(x => x.MidiasId).OrderByDescending(x => x.Count()).Take(amount).Select(x => x.Key).ToList();

            var midias = _dbContext.Midia.Where(x => queryTopMidias.Contains(x.Id)).Select(x => new MidiaVO { 
                Id = x.Id,
                Afinidade = x.Afinidade,
                DataLancamento = x.DataLancamento,
                ImagemURL = x.ImagemUrl,
                Sinopse = x.Sinopse,
                Tipo = x.Tipo,
                Titulo = x.Titulo
            }) ;

            return midias.ToList();
        
        }

        private List<MidiaVO> GetMidiasMock() {
            return new List<MidiaVO> { 
                new MidiaVO{ 

                    Id = 1,
                    DataLancamento = DateTime.Now,
                    Generos = new List<GeneroVO>{
                        new GeneroVO{ 
                            Id = 2,
                            Descricao = "Açao"
                        }
                    },
                    Sinopse = "asdasdhjkashdkj ahkajshd kjashd kjashd kahsdkhasd",
                    Titulo = "Rambo",
                    Tipo = Common.Enum.TipoMidia.Filme
                },
                new MidiaVO{

                    Id = 2,
                    DataLancamento = DateTime.Now,
                    Generos = new List<GeneroVO>{
                        new GeneroVO{
                            Id = 3,
                            Descricao = "Comedia"
                        }
                    },
                    Sinopse = "asdasdhjkashdkj ahkajshd kjashd kjashd kahsdkhasd",
                    Titulo = "Divertidos",
                    Tipo = Common.Enum.TipoMidia.Filme
                },
                new MidiaVO{

                    Id = 3,
                    DataLancamento = DateTime.Now,
                    Generos = new List<GeneroVO>{
                        new GeneroVO{
                            Id = 1,
                            Descricao = "Romance"
                        }
                    },
                    Sinopse = "asdasdhjkashdkj ahkajshd kjashd kjashd kahsdkhasd",
                    Titulo = "Oie",
                    Tipo = Common.Enum.TipoMidia.Filme
                }
            };
        }

        private List<ListaInteresseVO> GetListaInteresseMock() {
            return new List<ListaInteresseVO>()
            {
                new ListaInteresseVO{ 
                    DataAtualizacao = DateTime.Now,
                    DataCriacao = DateTime.Now,
                    Usuario = GetUsersMock().First(),
                    Midias = GetMidiasMock().Where(x => x.Id == 1).ToList(),
                    Id = 1
                }
            };
        }
        private List<UsuarioVO> GetUsersMock() {
            return new List<UsuarioVO>() { 
                new UsuarioVO{ 
                    Ativo = true,
                    DataCriacao = DateTime.Now,
                    DataNascimento = DateTime.Now.AddYears(-20),
                    Email = "",
                    Id = 1,
                    Nome = "Lazaro",
                    Perfil = Common.Enum.Perfil.Usuario,
                    Senha = "8888888888888",
                    Sexo = Common.Enum.Sexo.Feminino,
                    UltimoAcesso = DateTime.Now
                },
                new UsuarioVO{
                    Ativo = true,
                    DataCriacao = DateTime.Now,
                    DataNascimento = DateTime.Now.AddYears(-20),
                    Email = "",
                    Id = 2,
                    Nome = "Marcelo",
                    Perfil = Common.Enum.Perfil.Usuario,
                    Senha = "8888888888888",
                    Sexo = Common.Enum.Sexo.Feminino,
                    UltimoAcesso = DateTime.Now
                },
                new UsuarioVO{
                    Ativo = true,
                    DataCriacao = DateTime.Now,
                    DataNascimento = DateTime.Now.AddYears(-20),
                    Email = "",
                    Id = 3,
                    Nome = "Rafael",
                    Perfil = Common.Enum.Perfil.Usuario,
                    Senha = "8888888888888",
                    Sexo = Common.Enum.Sexo.Feminino,
                    UltimoAcesso = DateTime.Now
                },
                new UsuarioVO{
                    Ativo = true,
                    DataCriacao = DateTime.Now,
                    DataNascimento = DateTime.Now.AddYears(-20),
                    Email = "",
                    Id = 4,
                    Nome = "Lucas",
                    Perfil = Common.Enum.Perfil.Usuario,
                    Senha = "8888888888888",
                    Sexo = Common.Enum.Sexo.Feminino,
                    UltimoAcesso = DateTime.Now
                }
            };
        }
    }
}
