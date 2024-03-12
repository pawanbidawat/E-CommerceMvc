
$("document").ready(() => {    
    $(".delete").click(function () {
        debugger;
        var categoryId = $(this).attr('cat-id');

        console.log(categoryId)


        $.ajax({
            type: "DELETE",
            url: `https://localhost:44373/api/Category/DeleteCategory?id=${categoryId}`,


            success: function (response) {
                if (response) {
                    window.location.href = "/Shared/Add";

                }

            },
            error: function (xhr, textStatus, errorThrown) {

            }
        })
    })

    //adding delete method for SubCategory
   
})

