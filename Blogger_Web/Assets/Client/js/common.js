var common = {
    init: function () {
        common.registerEvents();
    },
    registerEvents: function () {
        $("#txtKeyword").autocomplete({
            minLength: 1,
            source: function (request, response) {
                $.ajax({
                    url: "/Home/GetListPostByTitle",
                    dataType: "json",
                    data: {
                        keyword: request.term
                    },
                    success: function (res) {
                        response(res.data);
                    }
                });
            },
            focus: function (event, ui) {
                $("#txtKeyword").val(ui.item.label);
                return false;
            },
            // Tự động gán giá trị khi hover title
            select: function (event, ui) {
                $("#txtKeyword").val(ui.item.label);
                //$("#project-id").val(ui.item.value);
                //$("#project-description").html(ui.item.desc);
                //$("#project-icon").attr("src", "images/" + ui.item.icon);

                return false;
            }
        }).autocomplete("instance")._renderItem = function (ul, item) {
            return $("<li>")
                .append("<div>" + item.label + "</div>")
                .appendTo(ul);
            //.append("<div>" + item.label + "<br>" + item.desc + "</div>")
        };
    }
};

common.init();