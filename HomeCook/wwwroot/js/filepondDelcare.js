var fileNames = new Array();
var uploadedImgBeRemoved = new Array();

function createFilePond(url, hiddenInputId ) {

    
    FilePond.registerPlugin(
        // encodes the file as base64 data
        FilePondPluginFileEncode,
        // validates the size of the file
        FilePondPluginFileValidateSize,
        // corrects mobile image orientation
        FilePondPluginImageExifOrientation,
        // previews dropped images
        FilePondPluginImagePreview
    );
    FilePond.setOptions({
        server: {
            process: url ,
            fetch: null,
            revert: null
        }

    });
    // Select the file input and use create() to turn it into a pond
    const pond = FilePond.create(document.querySelector('.filepond'));
    pond.on('addfile', (error, file) => {
        fileNames.push(file.filename);
        // filename.filename = "thao.jpg";
        convertFromListToInput(fileNames, hiddenInputId);

    });

    pond.on('removefile', (error, file) => {

        //  fileNames.delete(file.filename);
        // filename.filename = "thao.jpg";
        var index = -1;
        for (var i = 0; i < fileNames.length; i++) {
            if (fileNames[i] == file.filename) {
                index = i;
            }
        }
        if (index > -1) {
            fileNames.splice(index, 1);
        }

        convertFromListToInput(fileNames, hiddenInputId);

    });
}
function convertFromListToInput(listNames, elementId) {
    var fileList = '';
    if (listNames.length > 0) {
        fileList = listNames[0];
        for (var i = 1; i < listNames.length; i++) {
            fileList += ';' + listNames[i];
        }
    }

    document.getElementById(elementId).value = fileList;
    console.log(fileList);
}
