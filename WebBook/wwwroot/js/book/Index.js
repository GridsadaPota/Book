var Alldata = [];
// A $( document ).ready() block.
$(document).ready(function () {
    console.log("Welcome!!!");
    GatAll();
});

function LoadData(_data)
{
    $("#dataTables").DataTable({
        
        columns: [
            { data: 'book_Id' },
            { data: 'book_Code' },
            { data: 'book_Name' },
            { data: 'book_Author' },
            { data: 'price' },
            { data: 'publish_Date' },
            { data: 'create_Date' },
            { data: 'modify_Date' }
        ],
        data:_data
    });
}


async function GatAll()
{
    var url = $('#GetBookAll').data('request-url');
    console.log(url);
    $.ajax({
        type: 'GET',
        url: url,
        success: async function (res) {
            await console.log(res);
            Alldata = [];
            for (var i = 0; i < res.length; i++) {
                Alldata.push([
                    res[i]["book_Id"],
                    res[i]["book_Code"],
                    res[i]['book_Name'],
                    res[i]['book_Author'],
                    res[i]['price'],
                    null,
                    null,
                    null
                ]);
            }
            console.log(Alldata);
            await LoadData(Alldata);
        }
    });
    console.log("end")
}