app.controller('loginController', function ($scope, $http, $cookies) {
    this.usuario = usuario;
    this.erro = erro;
    $scope.aguarde = false;

    $cookies.remove('idUsuario');
    $cookies.remove('nomeUsuario');
    $cookies.remove('emailUsuario');

    this.acessar = function () {
        console.log(this.usuario);
        $scope.aguarde = true;

        $http.post('/api/usuario/logar', this.usuario)
            .then(function (retorno) {
                var dadosUsuario = retorno.data;

                this.erro.erro = dadosUsuario.Erro;
                this.erro.mensagens = dadosUsuario.Mensagem;

                if (!dadosUsuario.Erro) {
                    $cookies.put('idUsuario', dadosUsuario.Usuario.IdUsuario);
                    $cookies.put('nomeUsuario', dadosUsuario.Usuario.Nome);
                    $cookies.put('emailUsuario', dadosUsuario.Usuario.Email);

                    location.href = 'home/logar';
                }
                $scope.aguarde = false;
            })
            .catch(function (e) {
                console.log(e);
            });
    }

    this.criarConta = function () {
        location.href = 'home/CriarConta';
    }

    this.esqueciMinhaSenha = function () {
        location.href = 'home/EsqueciMinhaSenha';

    }
});

