app.controller('logadoController', function ($scope, $cookies) {
    $scope.nome = $cookies.get('nomeUsuario');
    $scope.email = $cookies.get('emailUsuario');
});

