var comment = {
    init: function () {
        comment.registerEvents();
    },
    registerEvents: function () {
        $('#btnCommentNew').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);

            var postid = btn.data('postid');
            var userid = btn.data('userid');

            var commentmsg = $('#txtCommentNew').val();

            if (commentmsg == "") {
                alert("Bình luận không được để trống");
                return;
            }
            $.ajax({
                url: "/Posts/AddNewComment",
                data: {
                    PostID: postid,
                    UserID: userid,
                    ParentID: 0,
                    CommentMsg: commentmsg
                },
                dataType: "json",
                type: "POST",
                success: function (response) {
                    if (response.status == true) {
                        //alert('Đã thêm bình luận');

                        //$("#comment_reload").load("/Posts/GetComment?postid=" + postid);
                        //$("#comment_reload").load("/Posts/Detail?id=" + 11);

                        location.reload(true);
                    }
                    else {
                        alert('Thêm bình luận lỗi');
                    }
                }
            });
        });

        $('.btnReply').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);

            var postid = btn.data('postid');
            var userid = btn.data('userid');
            var parentid = btn.data('parentid');

            var commentmsg = btn.data('commentmsg');
            var commentMsgValue = $('#' + commentmsg).val();

            if (commentMsgValue == "") {
                alert("Bình luận không được để trống");
                return;
            }
            $.ajax({
                url: "/Posts/AddNewComment",
                data: {
                    PostID: postid,
                    UserID: userid,
                    ParentID: parentid,
                    CommentMsg: commentMsgValue
                },
                dataType: "json",
                type: "POST",
                success: function (response) {
                    if (response.status == true) {
                        //alert('Đã thêm bình luận');

                        //$("#comment_reload").load("/Posts/GetComment?postid=" + postid);
                        //$("#comment_reload").load("/Posts/Detail?id=" + 11);

                        location.reload(true);
                    }
                    else {
                        alert('Thêm bình luận lỗi');
                    }
                }
            });
        });

    }
};

comment.init();