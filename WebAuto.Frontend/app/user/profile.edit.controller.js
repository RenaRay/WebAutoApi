(function () {
    'use strict';

    angular
        .module('app.user')
        .controller('app.user.profile.edit.controller', [
            'app.config',
            'app.services.authentication',
            'app.services.profile',
            '$location',
            '$timeout',
            'Upload',
            '$log',
            '$q',
            main
        ]);

    function main(config, authentication, profile, $location, $timeout, upload, $log, $q) {
        var vm = this;
        vm.data = {
            firstName: '',
            lastName: '',
            email: '',
            phone: '',
            contactsVisibleTo: '1',//TODO: create dictionary service and pull list of available values. 1=nobody
            avatar: null,
            cars: []
        };
        vm.files = [];//upload avatar
        vm.message = '';
        vm.savedSuccessfully = false;
        vm.save = save;
        vm.onFilesChanged = onFilesChanged;
        vm.addCar = addCar;
        vm.removeCar = removeCar;

        var login = authentication.status.login;
        profile
            .get(login)
            .then(function (results) {
                vm.data = results.data;
            }, function (error) {
                vm.message = error.data.error_description;
                vm.savedSuccessfully = false;
            });

        function save() {
            profile
                .post(vm.data)
                .then(function (results) {
                    uploadAvatar().then(function () {
                        vm.message = 'Профиль успешно сохранен. Через 2 секунды Вы будете перенаправлены на страницу просмотра Вашего профиля.';
                        vm.savedSuccessfully = true;
                        redirectToProfileView();
                    });
                }, function (error) {
                    vm.message = "Не удалось завершить сохранение профиля из-за ошибок: " + error.data.error_description;
                    vm.savedSuccessfully = false;
                });
        }

        function redirectToProfileView() {
            var timer = $timeout(function () {
                $timeout.cancel(timer);
                $location.path('/profile/'+authentication.status.login);
            }, 2000);
        }

        function uploadAvatar() {
            var deferred = $q.defer();
            if (vm.files.length == 0) {
                deferred.resolve();
                return deferred.promise;
            }
            var requestUrl = config.backendUrl + 'avatar';
            upload.upload({
                url: requestUrl,
                file: vm.files[0]
            }).progress(function (evt) {
                var progressPercentage = parseInt(100.0 * evt.loaded / evt.total);
                $log.log('progress: ' + progressPercentage + '% ' +
                            evt.config.file.name);
            }).success(function (response, status, headers, config) {
                $log.log('file ' + config.file.name + ' uploaded. Response: ' + JSON.stringify(response));
                vm.data.avatar = response.id;
                deferred.resolve();
            }).error(function(data, status, headers, config) {
                deferred.reject();
            });
            return deferred.promise;
        }

        function onFilesChanged(files) {
            if (!files || files.length < 1) {
                vm.data.avatar = null;
                return;
            }
            var file = files[0];
            resizeImage(file, function (blob) {
                file = blob;
                files[0] = blob;
            });
        }

        function resizeImage(file, onReady) {
            var MAX_IMAGE_SIZE = 250;

            var fileReader = new FileReader();
            fileReader.onload = function (e) {
                var image = new Image();
                image.onload = function () {
                    var canvas = document.createElement("canvas");
                    canvas.width = MAX_IMAGE_SIZE;
                    canvas.height = MAX_IMAGE_SIZE;

                    var minSize = Math.min(this.width, this.height);
                    var crop = {
                        x : this.width - minSize,
                        y : this.height - minSize
                    };
                    var context = canvas.getContext("2d");
                    context.drawImage(this, Math.round(crop.x/2), Math.round(crop.y/2), this.width - crop.x, this.height - crop.y, 0, 0, canvas.width, canvas.height);
                    if (onReady) {
                        var dataUrl = canvas.toDataURL();
                        var blob = dataUrlToBlob(dataUrl);
                        onReady(blob);
                    }
                }
                image.src = e.target.result;
            }
            fileReader.readAsDataURL(file);
        }

        function dataUrlToBlob(dataUrl) {
            // serialize the base64/URLEncoded data
            var byteString;
            if (dataUrl.split(',')[0].indexOf('base64') >= 0) {
                byteString = atob(dataUrl.split(',')[1]);
            }
            else {
                byteString = unescape(dataUrl.split(',')[1]);
            }

            // parse the mime type
            var mimeString = dataUrl.split(',')[0].split(':')[1].split(';')[0]

            // construct a Blob of the image data
            var array = [];
            for (var i = 0; i < byteString.length; i++) {
                array.push(byteString.charCodeAt(i));
            }
            return new Blob(
                [new Uint8Array(array)],
                { type: mimeString }
            );
        }

        function addCar() {
            vm.data.cars.push({
                plate: null,
                vendor: null,
                model: null
            });
        }

        function removeCar(index) {
            if (vm.data.cars.length == 0 ||
                index < 0 ||
                index >= vm.data.cars.length)
            {
                return;
            }
            vm.data.cars.splice(index, 1);
        }
    }
})();