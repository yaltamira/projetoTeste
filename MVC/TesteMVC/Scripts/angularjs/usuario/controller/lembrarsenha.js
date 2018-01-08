app.controller('lembrarSenhaController', function ($scope, $http, $cookies) {
    this.usuario = usuario;
    this.erro = erro;
    $scope.aguarde = false;

    this.enviar = function () {
        this.erro.erro = false;
        this.erro.mensagens = [];
        $scope.aguarde = true;
        $http.post('/api/usuario/ReenviarSenha?email=' + this.usuario.email)
            .then(function (retorno) {
                console.log(retorno);
                var dadosUsuario = retorno.data;

                this.erro.erro = dadosUsuario.Erro;
                this.erro.mensagens = dadosUsuario.Mensagem;

                $scope.aguarde = false;
            })
            .catch(function (e) {
                console.log(e);
                this.erro.mensagens = ['Ocorreu um erro ao tentar enviar o e-mail.'];
                $scope.aguarde = false;
            });
    }

    this.home = function () {
        location.href = '/';

    }
});

