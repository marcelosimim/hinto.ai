using Hinto.Domain.Contract;
using Hinto.Domain.VO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hinto.Domain.Service
{
    public class HintoAIDomain : IHintoAIDomain
    {
        public AIRecommendationVO Recommendations(long idUsuario)
        {
            var usuario = GetUsersMock().SingleOrDefault(x => x.Id == idUsuario);

            if (usuario == null)
                throw new Exception("Usuário não encontrado.");


            return new AIRecommendationVO { IdMidias = IAProcessRecommend(usuario) };
        }

        private List<long> IAProcessRecommend(UsuarioVO usuario)
        {
            var interessesUsuario = GetListaInteresseMock().SingleOrDefault(x => x.Usuario.Id == usuario.Id);

            //Se ele nao adicionou nada a lista, recomenda os tops.
            if (interessesUsuario.Midias.Count() == 0)
                return GetMidiasMock().Select(x => x.Id).ToList();


            var midiasids = new List<long>();
            //Por genero
            midiasids.AddRange(
                    GetMidiasMock().Where(x => x.Generos.Any(c => interessesUsuario.Midias.Any(z => z.Generos.Any(v => v.Descricao.Equals(c.Descricao))))).Select(x => x.Id)
                );
            //Por Ator
            midiasids.AddRange(
                   GetMidiasMock().Where(x => x.Artistas.Any(c => interessesUsuario.Midias.Any(z => z.Artistas.Any(v => v.Id == c.Id)))).Select(x => x.Id)
               );


            return midiasids;

        }

        private List<MidiaVO> GetMidiasMock() {
            return new List<MidiaVO> { 
                new MidiaVO{ 
                    Artistas = new List<ArtistaVO>
                    {
                        new ArtistaVO
                        {

                            Id = 1,
                            Nome = "Brad",
                            Profissao = "Ator"
                        }
                    },
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
                    Artistas = new List<ArtistaVO>
                    {
                        new ArtistaVO
                        {

                            Id = 2,
                            Nome = "Lary",
                            Profissao = "Ator"
                        }
                    },
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
                    Artistas = new List<ArtistaVO>
                    {
                        new ArtistaVO
                        {

                            Id = 3,
                            Nome = "Tyu",
                            Profissao = "Ator"
                        }
                    },
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
