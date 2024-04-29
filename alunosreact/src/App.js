import './App.css';
import React, { useState, useEffect } from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import axios from 'axios';
import { Modal, ModalBody, ModalFooter, ModalHeader } from 'reactstrap';
import logoCadastro from './assets/image.png';

function App() {
  
  const baseUrl="http://localhost:5227/api/alunos";
  const [data, setData] = useState([]);
  //'data' - usado para guardar o estado em si
  //'useState([])' - atribui um array vazio como valor inicial
  //'setData' - Variavel usada para atualizar o valor do estado
  const[modalIncluir, setModalIncluir]= useState(false);
  const[modalEditar, setModalEditar] = useState(false);
  const[modalExcluir, setModalExcluir] = useState(false);
  const[updateData, setUpdateData] = useState(true);

  const [alunoSelecionado, setAlunoSelecionado]=useState(
    {
      id:"",
      nome:"",
      email:"",
      idade:""
    })

  const abrirFecharModalIncluir=()=>{
    setModalIncluir(!modalIncluir);
  }
  
  const abrirFecharModalEditar=()=>{
    setModalEditar(!modalEditar);
  }

  const abrirFecharModalExcluir=()=>{
    setModalExcluir(!modalExcluir);
  }

  const handleChange=e=>{
  const {name, value}=e.target; 
  setAlunoSelecionado ({
  ...alunoSelecionado,[name]: value
  });
  console.log(alunoSelecionado);
  }
  
  const selecionarAluno=(aluno, opcao)=>{
    setAlunoSelecionado(aluno);
    (opcao==="Editar") ?
      abrirFecharModalEditar(): abrirFecharModalExcluir();
  }

  const pedidoGet = async()=>{
    await axios.get(baseUrl)
    .then(response=>{
      setData(response.data);
    }).catch(error=>{
      console.log(error);
    })
  }
  
  const pedidoPost = async()=>{
    delete alunoSelecionado.id;
    alunoSelecionado.idade=parseInt(alunoSelecionado.idade);
    await axios.post(baseUrl, alunoSelecionado)
    .then(response=>{
      setData(data.concat(response.data));
      setUpdateData(true);
      abrirFecharModalIncluir();
    }).catch(error=>{
      console.log(error);
    })
  }

  const pedidoPut = async () => {
    alunoSelecionado.idade = parseInt(alunoSelecionado.idade);
    alunoSelecionado.id = parseInt(alunoSelecionado.id);
    await axios.put(baseUrl + "/" + alunoSelecionado.id, alunoSelecionado)
      .then(response => {
        var resposta = response.data;
        setData(data.map(aluno => {
          if (aluno.id === alunoSelecionado.id) {
            return {
              ...aluno,
              nome: resposta.nome,
              email: resposta.email,
              idade: resposta.idade
            };
          }
          return aluno;
        }));
        setUpdateData(true);
        abrirFecharModalEditar();
      }).catch(error => {
        console.log(error);
      })
  }

  const pedidoDelete = async()=>{
    await axios.delete(baseUrl+"/"+alunoSelecionado.id)
    .then(response =>{
      setData(data.filter(aluno => aluno.id !== response.data));
      setUpdateData(true);
      abrirFecharModalExcluir();
    }).catch(error =>{
      console.log(error);
    })
  }

  useEffect(()=>{
    if (updateData) {
      pedidoGet();
      setUpdateData(false);
    }
  }, [updateData]);

  return (
    <div className="aluno-container">
      <br />
      <h3>Cadastro de Alunos</h3>
      <header>
        <img src={logoCadastro} alt="Cadastro" />
        <button className='btn btn-success' onClick={()=>abrirFecharModalIncluir()}>Incluir Novo Aluno</button>
      </header>
      <table className='table table-bordered'>
        <thead>
          <tr>
            <th>Id</th>
            <th>Nome</th>
            <th>Email</th>
            <th>Idade</th>
            <th>Operação</th>
          </tr>
        </thead>
        <tbody>
          {data.map(aluno=>(
            <tr key={aluno.id}>
              <td>{aluno.id}</td>
              <td>{aluno.nome}</td>
              <td>{aluno.email}</td>
              <td>{aluno.idade}</td>
              <td>
                <button className='btn btn-primary' onClick={()=>selecionarAluno(aluno, "Editar")}>Editar</button>{" "}
                <button className='btn btn-danger'  onClick={()=>selecionarAluno(aluno, "Excluir")}>Excluir</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
      <Modal isOpen={modalIncluir}>
      <ModalHeader>Incluir Alunos</ModalHeader>
        <ModalBody>
          <div className="form-group">
            <label>Nome: </label>
            <br/>
            <input type="text" className="form-control" name='nome' onChange={handleChange}/>
            <br />
            <label>Email: </label>
            <br />
            <input type="text" className="form-control" name='email' onChange={handleChange}/>
            <br />
            <label>Idade: </label>
            <br />
            <input type="text" className="form-control" name='idade' onChange={handleChange}/>
            <br />
          </div>
        </ModalBody>
        <ModalFooter>
          <button className="btn btn-primary" onClick={()=>pedidoPost()}>Incluir</button>{"  "}
          <button className="btn btn-danger" onClick={()=>abrirFecharModalIncluir()}>Cancelar</button>
        </ModalFooter>
      </Modal>
      <Modal isOpen={modalEditar}>
        <ModalHeader>Editar Aluno</ModalHeader>
        <ModalBody>
          <div className='form-group'>
            <label>ID: </label>
            <input type="text" readOnly value={alunoSelecionado && alunoSelecionado.id} />
            <br /><br />
            <label>Nome: </label>
            <input type="text" className='form-control' name='nome' onChange={handleChange}
                  value={alunoSelecionado && alunoSelecionado.nome}/><br />
            <label>Email: </label>
            <input type="text" className='form-control' name='email' onChange={handleChange}
                  value={alunoSelecionado && alunoSelecionado.email}/><br />
            <label>Idade: </label>
            <input type="text" className='form-control' name='idade' onChange={handleChange}
                  value={alunoSelecionado && alunoSelecionado.idade}/><br />
          </div>
        </ModalBody>
        <ModalFooter>
          <button className="btn btn-primary" onClick={()=>pedidoPut()}>Editar</button>{"  "}
          <button className="btn btn-danger" onClick={()=>abrirFecharModalEditar()}>Cancelar</button>
        </ModalFooter>
      </Modal>

      <Modal isOpen={modalExcluir}>
        <ModalHeader>Confirmar a exclusão deste(a) aluno(a): {alunoSelecionado && alunoSelecionado.nome}?</ModalHeader>
        <ModalFooter>
          <button className='btn btn-danger' onClick={()=>pedidoDelete()}> Sim </button>
          <button className='btn btn-secondary' onClick={()=>abrirFecharModalExcluir()}> Não </button>
        </ModalFooter>
      </Modal>

    </div>
  );

  
}

export default App;
