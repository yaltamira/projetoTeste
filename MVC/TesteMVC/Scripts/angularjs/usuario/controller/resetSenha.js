app.controller('resetSenhaController', function ($scope, $http, $cookies) {
    this.usuario = {
        idUsuario: 0,
        senha: '',
        senha2: ''
    };
    this.hash = {};
    $scope.usuarioValido = false;
    $scope.aguarde = false;
    $scope.erro = {
        erro: false,
        mensagens: []
    };
    $scope.atualizado = false;

    this.init = function (usuario, hash) {
        this.usuario.idUsuario = usuario;
        this.hash = hash;

        var usuarioEnvio = {
            IdUsuario: usuario,
            Senha: hash
        };
        $http.post('/api/usuario/ConfirmarResetSenha', usuarioEnvio)
            .then(function (retorno) {
                var validacao = retorno.data;

                $scope.erro.erro = validacao.Erro;
                $scope.erro.mensagens = validacao.Mensagem;
                $scope.usuarioValido = !validacao.Erro;
                $scope.aguarde = false;
            })
            .catch(function (e) {
                console.log(e);
            });

    }

    this.atualizar = function () {
        var altUsu = {
            IdUsuario: this.usuario.idUsuario,
            Senha: this.usuario.senha
        };

        $scope.aguarde = true;
        $http.post('/api/usuario/AlterarResetSenha', altUsu)
            .then(function (retorno) {
                var dadosUsuario = retorno.data;

                $scope.erro.erro = dadosUsuario.Erro;
                $scope.erro.mensagens = dadosUsuario.Mensagem;
                $scope.atualizado = !dadosUsuario.Erro;
 
                $scope.aguarde = false;
            })
            .catch(function (e) {
                console.log(e);
                $scope.aguarde = false;
            });
    }

    this.home = function () {
        location.href = '/';
    }

});

