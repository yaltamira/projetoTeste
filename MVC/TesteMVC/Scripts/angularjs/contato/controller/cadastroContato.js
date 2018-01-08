app.controller('cadastroContatoController', function ($scope, $http, $cookies) {

    $scope.contato = contato;
    this.erro = erro;

    this.init = function (id) {

        if (id === undefined || id === '')
            return;

        $http.get('/api/contato/SelecionaContato/' + id)
            .then(function (retorno) {
                var ret = retorno.data;
                $scope.contato = {
                    IdContato: ret.IdContato,
                    Nome: ret.Nome,
                    Email: ret.Email,
                    DDD: ret.DDD,
                    Telefone: ret.Telefone,
                    IdUsuario: ret.IdUsuario,
                    Ativo: ret.Ativo
                }
            })
            .catch(function (e) {
                console.log(e);
            });
    }

    this.gravar = function () {
        $scope.contato.IdUsuario = $cookies.get('idUsuario');

        $http.post('/api/contato/CadastrarOuAlterar', $scope.contato)
            .then(function (retorno) {
                this.erro.erro = retorno.data.Erro;

                if (this.erro.erro) {
                    this.erro.mensagens = retorno.data.Mensagem;
                } else {
                    location.href = '/contatos/contato';
                }
            })
            .catch(function (e) {
                console.log(e);
            });

    }

    this.cancelar = function () {
        location.href = 'contatos/contato';
    }

}).directive('telefone', function () {
    return {
        require: 'ngModel',
        link: function (scope, element, attr, ngModel) {
            var campo = ngModel.$name;
            switch (campo) {
                case ('txtDDD'):
                    ngModel.$validators.DDDTexto = DDDTexto;
                    ngModel.$validators.DDDValido = DDDValido;
                    break;
                case ('txtTelefone'):
                    ngModel.$validators.TelefoneValido = TelefoneValido;
                    break;

            }
        }
    }
    });

var DDDValido = function (modelValue, viewValue) {
    return viewValue && viewValue > 10 && viewValue <= 99;

}

var DDDTexto = function (modelValue, viewValue) {
    return !isNaN(viewValue);

}

var TelefoneValido = function (modelValue, viewValue) {
    if (isNaN(viewValue))
        return false;

    return true;
}