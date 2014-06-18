$(function () {
    $('.cloudinary-fileupload')
    .fileupload({
        dropZone: '#direct_upload',
        start: function () {
            $('.status_value').text('Starting direct upload...');
        },
        progress: function () {
            $('.status_value').text('Uploading...');
        },
    })
    .on('cloudinarydone', function (e, data) {
        $('.status_value').text('Idle');
        $.post('/Account/UploadDirect', data.result);
        var info = $('<div class="uploaded_info"/>');
        $('.uploaded_info_holder').empty();
        $(info).append($('<div class="image"/>').append(
          $.cloudinary.image(data.result.public_id, {
              format: data.result.format, width: 150, height: 150, crop: "fill"
          })
        ));
        $('.uploaded_info_holder').append(info);
    });
});