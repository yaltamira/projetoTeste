app.controller('atualizarCadastroController', function ($scope, $http, $cookies) {
    this.usuario = usuario;
    this.erro = erro;

    this.usuario.idUsuario = $cookies.get('idUsuario');
    this.usuario.nome = $cookies.get('nomeUsuario');
    this.usuario.email = $cookies.get('emailUsuario');

    this.atualizar = function () {

        var novoUsuario = {
            IdUsuario: this.usuario.idUsuario,
            Nome: this.usuario.nome,
            Email: this.usuario.email,
            Senha: this.usuario.senha
        };

        $http.post('/api/usuario/AlterarCadastro', novoUsuario)
            .then(function (retorno) {
                var dadosUsuario = retorno.data;

                this.erro.erro = dadosUsuario.Erro;
                this.erro.mensagens = dadosUsuario.Mensagem;

                if (!dadosUsuario.Erro) {
                    $cookies.put('nomeUsuario', dadosUsuario.Usuario.Nome);
                    $cookies.put('emailUsuario', dadosUsuario.Usuario.Email);
                    location.href = '/contatos/contato';
                }
            })
            .catch(function (e) {
                console.log(e);
            });
    }

});
