app.controller('cadastroController', function ($scope, $http, $cookies) {
    this.usuario = usuario;
    this.erro = erro;
    $scope.aguarde = false;

    this.verificaSenha = function () {
        this.erro.erro = false;
        this.erro.mensagens = [];

        if (this.usuario.senha != this.usuario.senha2) {
            this.erro.erro = true;
            this.erro.mensagens = ['As senhas informadas estão diferentes'];
        }
    }

    this.cadastrar = function () {
        $scope.aguarde = true;
        var novoUsuario = {
            Nome: this.usuario.nome,
            Email: this.usuario.email,
            Senha: this.usuario.senha
        };

        $http.post('/api/usuario/novocadastro', novoUsuario)
            .then(function (retorno) {
                var dadosUsuario = retorno.data;

                this.erro.erro = dadosUsuario.Erro;
                this.erro.mensagens = dadosUsuario.Mensagem;

                if (!dadosUsuario.Erro) {
                    location.href = '/';
                }
                $scope.aguarde = false;
            })
            .catch(function (e) {
                console.log(e);
            });
    }

    this.home = function () {
        location.href = '/';
    }
});
