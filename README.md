# Tên dự án: Blog IT

# Mô tả:
- Đây là dự án môn học: Phát triển dự án Angular JS.
- Web Blog IT là một trang web blog cá nhân về chủ đề Công nghệ thông tin. Sử dụng template có sẵn và thay đổi một số thứ để phù hợp với dự án. Ngoài ra, có sử dụng một số công nghệ như: **Bootstrap 3, JQuery, Ajax, AngularJS, .NET Core, CKEditor, ELFinder, ...**
- Về phía Cơ sở dữ liệu sử dụng: **SQL Server**
- Về phía Admin sử dụng: **AngularJS và API (viết bằng ASP.NET Core)**
- Về phía User sử dụng: **ASP.NET Core MVC**

# Sơ đồ thực thể liên kết: 
![image](https://github.com/lequanghao2002/Blogger/assets/113456985/7ecae806-3a64-43d0-956a-fc9cdcfb93ef)

# Chức năng:
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

# Giao diện và cách sử dụng:
#### Giao diện trang chủ
![image](https://github.com/lequanghao2002/Blogger/assets/113456985/f0d2e6fc-d695-4c2e-9e4d-4f3edd17a139)

Đây là trang chủ, giao diện mà khi người dùng vào trang web, ở phần trang chủ, bên trái sẽ có 1 sidebar hiển thị các chức năng đăng nhập và đăng ký, ở giữa thì sẽ hiển thị danh sách các bài viết và có phân trang với mỗi trang hiển thị 6 bài viết. Bên phải sẽ có phần tìm kiếm và phân loại bài theo danh mục
#### Giao diện đăng ký
![RegisterUser](https://github.com/lequanghao2002/Blogger/assets/113456985/0df70d4d-d537-4ba6-b817-1afb54f993f8)

Trang Đăng Ký giúp người dùng đăng ký tài khoản của mình, khi khách hàng điền đầy đủ thông tin và click đăng ký thì hệ thống sẽ kiểm tra thông tin đã hợp lệ chưa, nếu hợp lệ sẽ thông báo đăng ký tài khoản thành công cho người dùng, nếu không hợp lệ sẽ hiện thông báo lý do không đăng ký tài khoản được. Khi đăng ký thành công thì sẽ hiển thị thông báo đăng ký thành công và hệ thống gửi email thông báo đến email đăng ký

#### Giao diện đăng nhập
![LoginUser](https://github.com/lequanghao2002/Blogger/assets/113456985/62e079f0-a025-4378-b681-bef61e9b44af)

Trang Đăng Nhập sẽ cho phép người dùng đăng nhập vào tài khoản của mình để sử dụng những dịch vụ cần đăng nhập như bình luận, đăng ký nhận thông báo khi có bài viết mới, nếu đăng nhập sai thì sẽ hiện thị thông báo tương ứng.

#### Giao diện tìm kiếm bài viết theo tiêu đề và theo danh mục
https://github.com/lequanghao2002/Blogger/assets/113456985/fa5e6d8b-3ea8-41d8-8116-0931900e45d3

Khi người dùng nhập từ khóa vào ô tìm kiếm thì nó sẽ gợi ý ra những bài viết có chứa từ khóa đó, khi người dùng bấm vào icon kính lúp thì nó sẽ hiển thị ra trang tìm kiếm và kết quả
Khi bấm vào 1 danh mục bất kì thì nó sẽ hiển thị ra trang phân loại và hiển thị có bao nhiêu bài viết thuộc danh mục đó.

#### Giao diện chi tiết bài viết, bình luận và phản hồi
https://github.com/lequanghao2002/Blogger/assets/113456985/8af58e26-33aa-4e43-9542-9cd189c604c4

Khi bấm vào đọc thêm thì nó sẽ vào trang chi tiết bài viết, ở phần chi tiết bài viết nó sẽ hiển thị đầy đủ thông tin hơn, và ở cuối bài viết nó sẽ hiển thị ra những bài viết có liên quan đến và sau phần bài viết liên quan thì sẽ có những bình luận của bài viết
Khi chưa đăng nhập thì phần bình luận nó chỉ được xem và không được bình luận và phản hồi. Còn khi đăng nhập thành công thì nó sẽ hiển thị ra phần bình luận cho mình và phần phản hồi bình luận

#### Giao diện nhận thông báo khi có bài viết mới


Khi mà mình chưa đăng nhập thì dưới phần danh mục không có phần đăng ký nhận thông báo khi có bài viết mới. 
Còn sau khi đã đăng nhập thì nó sẽ hiển thị phần đăng ký cho mình, nếu đăng ký thành công thì nó sẽ hiển thị thông báo đăng ký thành công, sau khi đăng ký thành công mỗi lần có bài viết mới thì hệ thống sẽ gửi link bài viết mới đến những email đăng ký nhận thông báo
