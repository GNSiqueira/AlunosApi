import React from "react";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import Login from './pages/Login';
import Alunos from './pages/Alunos';
import NovoAluno from './pages/NovoAluno/index';

export default function Routess(){
    return(
        <BrowserRouter>
            <Routes>
                <Route path="/" exact Component={Login}/>
                <Route path="/alunos" Component={Alunos}/>
                <Route path="/aluno/novo/:alunoId" Component={NovoAluno}/>
            </Routes>
        </BrowserRouter>
    );
}