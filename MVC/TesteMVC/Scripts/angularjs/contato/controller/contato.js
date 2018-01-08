app.controller('contatoController', function ($scope, $http, $cookies) {

    this.contato = contato;
    this.erro = erro;
    this.idUsuario = $cookies.get('idUsuario');
    AtualizarLista($scope, $http, this.idUsuario);
    $scope.aguarde = false;

    this.excluirUsuario = function (id) {
        $scope.aguarde = true;
        $http.delete('/api/contato/deletar/' + id)
            .then(function () {
                AtualizarLista($scope, $http, $cookies.get('idUsuario'));
            })
            .catch(function (e) {
                console.log(e);
                $scope.aguarde = false;
            });
    }
});

var AtualizarLista = function ($scope, $http, idUsuario) {
    $scope.aguarde = true;
    $http.get('/api/contato/ListaContatos/' + idUsuario)
        .then(function (retorno) {
            $scope.listaContatos = retorno.data;
            $scope.aguarde = false;
        })
        .catch(function (e) {
            console.log(e);
            $scope.aguarde = false;
        });
}