# Tên dự án: Blog IT

# Mô tả:
- Đây là dự án môn học: Phát triển dự án Angular JS.
- Web Blog IT là một trang web blog cá nhân về chủ đề Công nghệ thông tin. Sử dụng template có sẵn và thay đổi một số thứ để phù hợp với dự án. Ngoài ra, có sử dụng một số công nghệ như: **Bootstrap 3, JQuery, Ajax, AngularJS, .NET Core, CKEditor, ELFinder, ...**
- Về phía Cơ sở dữ liệu sử dụng: **SQL Server**
- Về phía Admin sử dụng: **AngularJS và API (viết bằng ASP.NET Core)**
- Về phía User sử dụng: **ASP.NET Core MVC**

# Sơ đồ thực thể liên kết: 
![image](https://github.com/lequanghao2002/Blogger/assets/113456985/7ecae806-3a64-43d0-956a-fc9cdcfb93ef)

# Các chức năng hiện có:
### Admin: 
- Chức năng đăng nhập, đăng xuất
- Chức năng tạo, sửa, xóa và xem chi tiết bài viết (Post). Tìm kiếm theo tiêu đề, tìm kiếm theo ngày, phân loại bài viết theo danh mục, phân trang và hiển thị số trang/tổng số trang
- Chức năng tạo, sửa, xóa danh mục (Category), phân trang và hiển thị số trang/tổng số trang
- Chức năng tạo, sửa, xóa vai trò (Role), phân trang và hiển thị số trang/tổng số trang

### User:
- Chức năng đăng nhập, đăng ký, đăng xuất
- Chức năng tìm kiếm bài viết theo tên, theo danh mục
- Chức năng phân trang
- Chức năng xem chi tiết bài viết
- Chức năng bình luận và phản hồi bình luận
- Chức năng nhận thông báo khi có bài viết mới

# Hình ảnh, video mô tả của các chức năng:
### Giao diện trang chủ
![image](https://github.com/lequanghao2002/Blogger/assets/113456985/f0d2e6fc-d695-4c2e-9e4d-4f3edd17a139)

Đây là trang chủ, giao diện mà khi người dùng vào trang web, ở phần trang chủ, bên trái sẽ có 1 sidebar hiển thị các chức năng đăng nhập và đăng ký, ở giữa thì sẽ hiển thị danh sách các bài viết và có phân trang với mỗi trang hiển thị 6 bài viết. Bên phải sẽ có phần tìm kiếm và phân loại bài theo danh mục
### Giao diện và chức năng đăng ký
![RegisterUser](https://github.com/lequanghao2002/Blogger/assets/113456985/0df70d4d-d537-4ba6-b817-1afb54f993f8)

##### Trang Đăng Ký giúp người dùng đăng ký tài khoản của mình, khi khách hàng điền đầy đủ thông tin và click đăng ký thì hệ thống sẽ kiểm tra thông tin đã hợp lệ chưa, nếu hợp lệ sẽ thông báo đăng ký tài khoản thành công cho người dùng, nếu không hợp lệ sẽ hiện thông báo lý do không đăng ký tài khoản được. Khi đăng ký thành công thì sẽ hiển thị thông báo đăng ký thành công và hệ thống gửi email thông báo đến email đăng ký

### Giao diện và chức năng đăng nhập
![LoginUser](https://github.com/lequanghao2002/Blogger/assets/113456985/62e079f0-a025-4378-b681-bef61e9b44af)

##### Trang Đăng Nhập sẽ cho phép người dùng đăng nhập vào tài khoản của mình để sử dụng những dịch vụ cần đăng nhập như bình luận, đăng ký nhận thông báo khi có bài viết mới, nếu đăng nhập sai thì sẽ hiện thị thông báo tương ứng.

### Giao diện và chức năng tìm kiếm bài viết theo tiêu đề và theo danh mục
https://github.com/lequanghao2002/Blogger/assets/113456985/fa5e6d8b-3ea8-41d8-8116-0931900e45d3

##### Khi người dùng nhập từ khóa vào ô tìm kiếm thì nó sẽ gợi ý ra những bài viết có chứa từ khóa đó, khi người dùng bấm vào icon kính lúp thì nó sẽ hiển thị ra trang tìm kiếm và kết quả
##### Khi bấm vào 1 danh mục bất kì thì nó sẽ hiển thị ra trang phân loại và hiển thị có bao nhiêu bài viết thuộc danh mục đó.

### Giao diện và chức năng chi tiết bài viết, bình luận và phản hồi
https://github.com/lequanghao2002/Blogger/assets/113456985/8af58e26-33aa-4e43-9542-9cd189c604c4

##### Khi bấm vào đọc thêm thì nó sẽ vào trang chi tiết bài viết, ở phần chi tiết bài viết nó sẽ hiển thị đầy đủ thông tin hơn, và ở cuối bài viết nó sẽ hiển thị ra những bài viết có liên quan đến và sau phần bài viết liên quan thì sẽ có những bình luận của bài viết
##### Khi chưa đăng nhập thì phần bình luận nó chỉ được xem và không được bình luận và phản hồi. Còn khi đăng nhập thành công thì nó sẽ hiển thị ra phần bình luận cho mình và phần phản hồi bình luận

### Giao diện và chức năng nhận thông báo khi có bài viết mới
[Do dung lượng video lớn không tải lên được. Vui lòng bấm vào đây để xem](https://drive.google.com/file/d/1fZxItacBqQ8ZcUeOJWWveJW4vtXplE8V/view?usp=sharing)

##### Khi mà mình chưa đăng nhập thì dưới phần danh mục không có phần đăng ký nhận thông báo khi có bài viết mới. Còn sau khi đã đăng nhập thì nó sẽ hiển thị phần đăng ký cho mình, nếu đăng ký thành công thì nó sẽ hiển thị thông báo đăng ký thành công, sau khi đăng ký thành công mỗi lần có bài viết mới thì hệ thống sẽ gửi link bài viết mới đến những email đăng ký nhận thông báo

### Giao diện và chức năng đăng nhập, thêm, xóa, sửa, xem chi tiết bài viết của Admin
[Do dung lượng video lớn không tải lên được. Vui lòng bấm vào đây để xem](https://drive.google.com/file/d/1_sP5EasQkn_n8iXr5BJZH0Pxg_d5qJ5y/view?usp=sharing)

##### Khi vào trang của admin thì việc đầu tiên là mình phải đăng nhập bằng tài khoản dành cho admin.
##### Tạo bài viết: Để tạo bài viết mới bấm vào nút Create, ở phần Content là phần nội dung chính có sử dụng ckeditor để quản lí nội dung của bài viết. Còn ở phần chọn hình ảnh thì sử dụng elfinder dùng để quản lí hình ảnh. Khi mà chưa nhập đầy đủ thông tin thì nút Create sẽ bị disabled không bấm vào được, còn khi đã điền đầy đủ thông tin thì nút Create nó sẽ hiện lên.
##### Xóa bài viết: Để xóa bài viết cần bấm vào icon thùng rác nó sẽ hiển thị hộp thoại xác nhận, nếu muốn xóa thì bấm ok còn muốn hủy thì bấm cancel
##### Sửa bài viết: Để chỉnh sửa bài viết cần bấm vào icon cây bút
##### Xem chi tiết bài viết:	Để xem chi tiết bài viết cần bấm vào icon chữ I nó sẽ vào trang chi tiết của bài viết đó, ở trang chi tiết bài viết sẽ hiển thị thông tin đầy đủ hơn

### Giao diện và chức năng tìm kiếm bài viết theo ngày, theo tiêu đề, theo danh mục của bài viết
https://github.com/lequanghao2002/Blogger/assets/113456985/23e9a838-102f-4d05-a657-c4904e7b7dd7

##### Tìm kiếm theo ngày: Phần tìm kiếm theo ngày sẽ có 2 chức năng tương ướng với 2 ô ngày, ô bên trái thì sẽ hiển thị những bài viết có sau ngày đó. Còn với ô bên phải thì nó sẽ hiển thị những bài viết có trước ngày đó.
##### Tìm kiếm theo danh mục: Khi chọn danh mục và bấm vào tìm kiếm thì nó sẽ hiển thị những bài viết tương ứng có chứa danh mục đó
##### Tìm kiếm theo tiêu đề bài viết: Khi nhập từ khóa vào phần tìm kiếm thì nó sẽ hiển thị những bài viết có tiêu đề chứa từ khóa đó

### Giao diện và chức năng xem tất cả danh mục, hiển thị số bài viết/tổng số bài viết và phân trang
https://github.com/lequanghao2002/Blogger/assets/113456985/b430ca34-1aa2-4f30-972b-fbc4c78bcba1

##### Để xem tất cả danh mục của bài viết thì bấm vào nút Show của phần category nó sẽ hiển thị những danh mục thuộc bài viết đó
##### Phần phân trang sẽ hiển thị 6 bài viết trên 1 trang

### Giao diện và chức năng của Danh mục và Vai trò
![image](https://github.com/lequanghao2002/Blogger/assets/113456985/25540efb-829b-4d90-b444-e4f81ebc58cb)

![image](https://github.com/lequanghao2002/Blogger/assets/113456985/0cd335fa-a067-4aaa-b427-ddb22fd1d49d)

##### Về phần danh mục và vai trò hiện tại chỉ có các chức năng như thêm, xóa, sửa

