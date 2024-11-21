using Xunit;
using AlunosApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace AlunoApi.Tests
{
    public class AlunoTestesCrud
    {
        private readonly List<Aluno> _baseDeDadosFalsa;

        public AlunoTestesCrud()
        {
            // Inicializa a base de dados simulada
            _baseDeDadosFalsa = ConfigurarBancoDeDados();
        }

        private List<Aluno> ConfigurarBancoDeDados()
        {
            return new List<Aluno>
            {
                new Aluno { Id = 1, Nome = "Calos", Email = "carlos@email.com", Idade = 20 },
                new Aluno { Id = 2, Nome = "Rovertion", Email = "rovertion@email.com", Idade = 22 }
            };
        }

        [Fact]
        public void RetornarTodosAlunos_DeveRetornarListaCompleta()
        {
            // Arrange
            int totalEsperado = _baseDeDadosFalsa.Count;

            // Act
            var alunosRetornados = _baseDeDadosFalsa.ToList();

            // Assert
            Assert.NotEmpty(alunosRetornados);
            Assert.Equal(totalEsperado, alunosRetornados.Count);
        }

        [Fact]
        public void BuscarAlunoPorId_DeveRetornarAlunoEspecifico()
        {
            // Arrange
            int idAlunoBuscado = 1;

            // Act
            var aluno = ProcurarAlunoPorId(idAlunoBuscado);

            // Assert
            Assert.NotNull(aluno);
            Assert.Equal(idAlunoBuscado, aluno.Id);
        }

        private Aluno ProcurarAlunoPorId(int id)
        {
            return _baseDeDadosFalsa.SingleOrDefault(a => a.Id == id);
        }

        [Fact]
        public void InserirAluno_DeveAdicionarAlunoNaBase()
        {
            // Arrange
            var alunoParaAdicionar = CriarAluno("Pedro", "pedro@email.com", 25);

            // Act
            AdicionarNaBase(alunoParaAdicionar);
            var alunoCadastrado = ProcurarAlunoPorId(alunoParaAdicionar.Id);

            // Assert
            Assert.NotNull(alunoCadastrado);
            Assert.Equal(alunoParaAdicionar.Nome, alunoCadastrado.Nome);
        }

        private Aluno CriarAluno(string nome, string email, int idade)
        {
            return new Aluno { Nome = nome, Email = email, Idade = idade };
        }

        private void AdicionarNaBase(Aluno aluno)
        {
            _baseDeDadosFalsa.Add(aluno);
        }

        [Fact]
        public void AlterarAluno_DeveModificarDadosDoAlunoExistente()
        {
            // Arrange
            int idDoAlunoParaAlterar = 1;
            string nomeAtualizado = "Calos Modificado";

            // Act
            AtualizarNomeDoAluno(idDoAlunoParaAlterar, nomeAtualizado);

            // Assert
            var alunoAtualizado = ProcurarAlunoPorId(idDoAlunoParaAlterar);
            Assert.NotNull(alunoAtualizado);
            Assert.Equal(nomeAtualizado, alunoAtualizado.Nome);
        }

        private void AtualizarNomeDoAluno(int id, string novoNome)
        {
            var aluno = _baseDeDadosFalsa.SingleOrDefault(a => a.Id == id);
            if (aluno != null)
            {
                aluno.Nome = novoNome;
            }
        }

        [Fact]
        public void ExcluirAluno_DeveRemoverAlunoDaBase()
        {
            // Arrange
            int idDoAlunoExcluir = 1;

            // Act
            RemoverAlunoDaBase(idDoAlunoExcluir);

            // Assert
            var alunoExcluido = ProcurarAlunoPorId(idDoAlunoExcluir);
            Assert.Null(alunoExcluido);
        }

        private void RemoverAlunoDaBase(int id)
        {
            var aluno = _baseDeDadosFalsa.SingleOrDefault(a => a.Id == id);
            if (aluno != null)
            {
                _baseDeDadosFalsa.Remove(aluno);
            }
        }
    }
}
