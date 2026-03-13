class Usuario
{
    UsuarioModel()
    {
        GUID idUsuario
        string Nome
        string Sobrenome
        DateOnly DataNascimento
        string Foto
        List<TipoPerfil> TipoPerfil = TipoPerfil.Paciente
        bool PerfilAtivo = true
        int CPF
        Genero generoNome
        int QtdAcessos
        int Telefone
}
    UsuarioController()
    {
        // POST: api/usuario/ativar             -> TornarProfissional(idUsuario)
        // POST: api/usuario             -> CriarUsuario() & CriarPacienteAuto(idUsuario)
        //  GET: api/usuario/{idUsuario}   -> VisualizarPerfil(idUsuario)
        //  PUT: api/usuario/{idUsuario}   -> AtualizarPerfil(idUsuario)
        //PATCH: api/usuario/{idUsuario}/desativar -> DesativarConta(idUsuario)
    }
    UsuarioService()
    {
        public ok TornarProfissional(idUsuario) { /*é nesse metodo que se adiciona um segundo perfil ao usuario (sendo o primeiro paciente por padrao, o segundo perfil(no caso perfil profissional é opcional))*/}
        public OK CriarUsuario() { }
        public void CriarProfissionalAuto(idUsuario) { }
        public UsuarioDto VisualizarPerfilUsuario(idUsuario) { }
        public UsuarioAtualizadoDto AtualizarPerfilUsuario(idUsuario) { }
        public OK DesativarConta(idUsuario) {/*Muda o campo de inativo para true*/ }
    }
    UsuarioDtos()
    {
        UsuarioDto{
            string idUsuario
             string Nome
          string NomeCompleto
         DateOnly DataNascimento
         string Foto
         List<TipoPerfil> TipoPerfil
         bool PerfilProfissionalAtivo
         int CPF
         int generoNome
         int QtdAcessos
         int Telefone
         }

        UsuarioAtualizadoDto{
            string Nome
         string Sobrenome
        string Foto
        int generoNome
                int telefone
        }
    }
}

class Paciente
{
    PacienteModel()
    {
        string idPaciente(FK idUsuario)
        List<Prontuario> Prontuarios
        List<Consulta> Consultas
        List<RPD> RegistrosPensamentosDifuncionais
        //List<ProfissionaisDto> MeusProfissionais
}
    PacienteController()
    {
        // GET: api/paciente/{idPaciente}/   -> VisualizarPerfil(idPaciente)
        // PUT: api/paciente/{idPaciente} -> AtualizarPerfil(idUsuario)

    }
    PacienteService()
    {
        public void CriarPacienteAuto(idUsuario) {/*chamado em CriarUsuario()*/}
        public PacienteDto VisualizarPerfil(idPaciente)
        public PacienteAtualizadoDto AtualizarPerfil(idPaciente)
    }

    PacienteDtos()
    {

        PacienteDto{
            string idPaciente(FK idUsuario)
        List<Prontuario> Prontuarios
        List<Consulta> Consultas
        List<RPD> RegistrosPensamentosDifuncionais
        //List<ProfissionaisDto> MeusProfissionais
        }
        PacienteAtualizadoDto{
            List<RPD> RegistrosPensamentosDifuncionais
        }
    }
}
class Psicologo
{
    PsicologoModel()
    {
        string idPsicologo(FK idUsuario)
        string CRP
        string Descricao
        ModalidadeAtendimento ModalidadeDeAtendimento
        List<TipoPaciente> TiposPacientes
        List<CondicaoTerapeutica> CondicoesTerapeuticas
            int TelefoneProfissional
}
    PsicologoController()
    {
        // GET: api/psicologos                  -> MostrarTodosPsicologos()
        // GET: api/psicologos/filtros          -> FiltrarPsicologos(modalidadeIds, abordagemIds, condicaoIds, publicoIds)
        // GET: api/psicologos/nomeOuCRP        -> BuscarPsicologoPorNomeOuCRP(nomeOuCRP)
        // GET: api/psicologos/{idPsicologo}/   -> VisualizarPerfil(idPsicologo)
        // PUT: api/psicologos/{idPsicologo}    -> AtualizarPerfil(idPsicologo)

    }
    PsicologoService()
    {
        public List<PsicologoDto> MostrarTodosPsicologos()
        public List<PsicologoDto> FiltrarPsicologos(modalidadeIds, abordagemIds, condicaoIds, publicoIds)
        public List<PsicologoDto> BuscarPsicologoPorNomeOuCRP(nomeOuCRP)
        public PsicologoDto VisualizarPerfil(idPsicologo)
        public PsicologoAtualizadoDto AtualizarPerfil(idPsicologo)
    }
    PsicologoDtos()
    {
        PsicologoDto{
            string idPsicologo(FK idUsuario)
            string NomeCompleto
            string Foto
            int generoNome
            int telefonePessoal
            string Nome
            string CRP
            string Descricao
            ModalidadeAtendimento ModalidadeDeAtendimento
            List<TipoPaciente> TiposPacientes
            List<CondicaoTerapeutica> CondicoesTerapeuticas
                int TelefoneProfissional
        }
        PsicologoAtualizadoDto{
            string Descricao
            ModalidadeAtendimento ModalidadeDeAtendimento
            List<TipoPaciente> TiposPacientes
            List<CondicaoTerapeutica> CondicoesTerapeuticas
                int TelefoneProfissional
        }
    }
}
class Prontuario
{
    ProntuarioModel()
    {
        GUID idProntuario
        string idProfissional
        string idPaciente
        DateTime DataCriacao
        bool Status (Ativo / Arquivado)
        List<Consulta> Consultas
        int ContatoEmergencia
        string HistoricoPaciente
        DateTime DataUltimaAtualizacao
        string UsoDeMedicamentos
    }
    ProntuarioController()
    {
        // GET: api/prontuario/{idPaciente}                     -> ExibirTodosProntuariosParaPaciente(idPaciente)
        // GET: api/prontuario/{idPaciente}/{idProntuario}      -> ExibirProntuarioParaPaciente(idPaciente, idProntuario)

        // GET: api/prontuario/{idProfissional}                 ->  ListarProntuariosPorProfissional(idProfissional)
        // GET: api/prontuario/{idProfissional/{idProntuario}   ->  ExibirProntuarioParaProfissional(idProfissional, idProntuario)
        // PUT: api/prontuario/{idProntuario}                   ->  AtualizarProntuario(idProntuario)
        // POST: api/prontuario/{idProfissional}/{idPaciente}   ->  CriarProntuario(idProfissional, idPaciente)
    }
    ProntuarioService()
    {
        public InfosBasicasProntuarioDto ExibirProntuarioParaPaciente(idPaciente, idProntuario )
        public List<InfosBasicasProntuarioDto> ExibirTodosProntuariosParaPaciente(idPaciente)

        public TodasInfosProntuarioDto ExibirProntuarioParaProfissional(idProfissional, idProntuario)
        public List<TodasInfosProntuarioDto> ListarProntuariosPorProfissional(idProfissional)
        public ProntuarioAtualizadoDto AtualizarProntuario(AtualizarProntuarioDto)
        public ok CriarProntuario(idProfissional, idPaciente, TodasInfosProntuarioDto)
    }

    ProntuarioDtos()
    {
        InfosBasicasProntuarioDto{
            GUID idProntuario
            string NomeProfissional
            string NomePaciente
            DateTime DataCriacao
            List<Consulta> Consultas
            int ContatoEmergencia
            string UsoDeMedicamentos
        }
        AtualizarProntuarioDto{
            int ContatoEmergencia
            string HistoricoPaciente
            DateTime DataUltimaAtualizacao
            string UsoDeMedicamentos
        }
        TodasInfosProntuarioDto{
            GUID idProntuario
            string NomeProfissional
            string NomePaciente
            DateTime DataCriacao
            bool Status(Ativo / Arquivado)
            List<Consulta> Consultas
            int ContatoEmergencia
            string HistoricoPaciente
            DateTime DataUltimaAtualizacao
            string UsoDeMedicamentos
        }
    }
}
class Consulta
{
    ConsultaModel()
    {
        GUID idConsulta
        string idPaciente
        string idProfissional
        string Prescricao
        Datetime DataHoraConsulta
        string Resumo


    }
    ConsultaController()
    {
        //  GET: api/consulta/{idPaciente}                     -> ExibirTodasConsultasParaPaciente(idPaciente)
        //  GET: api/consulta/{idPaciente}/{idConsulta}        -> ExibirConsultaParaPaciente(idPaciente, idConsulta)

        //  GET: api/consulta/{idProfissional}                 ->  ListarConsultasPorProfissional(idProfissional)
        //  GET: api/consulta/{idProfissional/{idConsulta}     ->  ExibirConsultaParaProfissional(idProfissional, idConsulta)
        //  PUT: api/consulta/{idConsulta}                     ->  EditarTemporariamenteConsulta(TodasInfosConsultaDto)      --> O profissional deixa de poder editar apos o fim da consulta
        // POST: api/consulta/{idProfissional}/{idPaciente}    ->  CriarConsulta(TodasInfosConsultaDto)
    }
    ConsultaService()
    {
        public InfosBasicasConsultaDto ExibirConsultaParaPaciente(idPaciente, idConsulta )
        public List<InfosBasicasConsultaDto> ExibirTodasConsultasParaPaciente(idPaciente)

        public TodasInfosConsultaDto ExibirConsultaParaProfissional(idProfissional, idConsulta)
        public List<TodasInfosConsultaDto> ListarConsultasPorProfissional(idProfissional)
        public AtualizarConsultaDto EditarTemporariamenteConsulta(AtualizarConsultaDto)
        public ok CriarConsulta(TodasInfosConsultaDto, idProfissional, idPaciente )
    }
    ConsultaDtos()
    {
        InfosBasicasConsultaDto{
            GUID idConsulta
        string idPaciente
        string idProfissional
        string Prescricao
        Datetime DataHoraConsulta
        }
        TodasInfosConsultaDto{
            GUID idConsulta
        string idPaciente
        string idProfissional
        string Prescricao
        Datetime DataHoraConsulta
        string Resumo
        }
        AtualizarConsultaDto{
        string Prescricao
        string Resumo
        }
    }
}


class RegistroPensamentoDisfuncional { }
class RegistroEmocao { }

#region Modelos

class Profissional
{
    ProfissionalModel()
    {
        //dados Profissional
    }
    ProfissionalController()
    {
        // GET: api/profissional                        -> MostrarTodosprofissionais()
        // GET: api/profissional/filtros                -> FiltrarProfissionais()
        // GET: api/profissional/nomeOuRegistro         -> BuscarProfissionalPorNomeOuRegistro(nomeOuRegistro)
        // GET: api/profissional/{idProfissional}/      -> VisualizarPerfil(idProfissional)
        // PUT: api/profissional/{idProfissional}       -> AtualizarPerfil(idProfissional)

    }
    ProfissionalService()
    {
        public void CriarProfissionalAuto(idUsuario) {/*chamado em CriarUsuario()*/}
        public ProfissionalDto VisualizarPerfil(idProfissional)
        public ProfissionalAtualizadoDto AtualizarPerfil(idProfissional)
    }

    ProfissionalDtos()
    {
        ProfissionalDto{
            //dados do ProfissionalDTO
        }
        ProfissionalAtualizadoDto{
            //dados do ProfissionalAtualizadoDTO
        }
    }
}




class ADeusDara
{
    Models()
    {
        Usuario();
        Paciente();
        Profissional();
        Prontuario();
        Consulta();
    }
    Controller()
    {
        Usuario();
        Paciente();
        Profissional();
        Prontuario();
        Consulta();
    }
    Services()
    {
        Usuario();
        Paciente();
        Profissional();
        Prontuario();
        Consulta();
    }
    Dtos()
    {
        Usuario();
        Paciente();
        Profissional();
        Prontuario();
        Consulta();
    }
}

#endregion
