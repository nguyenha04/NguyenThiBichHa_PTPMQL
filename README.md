1.	Cấu trúc thư mục của dự án MVC:
•	Controllers: Chứa các lớp Controller xử lý yêu cầu HTTP.
•	Models: Chứa các lớp Model đại diện cho dữ liệu và logic.
•	Views: Chứa các file giao diện .cshtml. Thường có thư mục con theo tên Controller (ví dụ: Views/Home) và thư mục Shared cho các view dùng chung (ví dụ: _Layout.cshtml).
•	wwwroot: Thư mục gốc cho tài nguyên tĩnh (CSS, JS, ảnh).
•	Properties: Cấu hình chạy ứng dụng (launchSettings.json).
•	appsettings.json: Chứa các cài đặt ứng dụng, chuỗi kết nối.
•	Program.cs: Điểm khởi đầu và cấu hình ứng dụng .NET Core.
•	bin & obj: Thư mục chứa file biên dịch và file tạm. 

2.	Định tuyến (Routing) trong .NET MVC
•	MVC sẽ gọi bộ điều khiển (Controller) và các hành động bên trong (Action) thông qua URL
•	Logic định tuyến MVC sử dụng dạng: /[Controller]/[Action]/[Parameters]
•	Định tuyến được cấu hình trong file Program.cs:

3.	Namespace trong C#
Trong C#, namespace (không gian tên) là một cơ chế để tổ chức code theo logic, nhóm các lớp, cấu trúc, enum và các thành phần khác vào các nhóm có tên, giúp tránh xung đột tên và quản lý dự án lớn dễ dàng hơn, hoạt động như những "hộp chứa" phân cấp, sử dụng từ khóa namespace để định nghĩa và using để sử dụng. 
•	Tổ chức và quản lý: Nhóm các thành phần liên quan lại với nhau (ví dụ: System.Collections chứa các kiểu dữ liệu collection).
•	Tránh xung đột tên: Cho phép có nhiều lớp tên giống nhau nhưng nằm trong các namespace khác nhau (ví dụ: System.IO.File và System.Windows.Forms.File).
•	Phân cấp: Hỗ trợ namespace lồng nhau (ví dụ: Microsoft.NET.Tasks). 
•	Định nghĩa:
Sử dụng từ khóa namespace và đặt tên nhóm.
namespace MyCompany.MyApp.Utilities
{
    public class Helper
    {
        // ...
    }
}
•	Sử dụng:
o	Fully Qualified Name (Tên đầy đủ): Gọi trực tiếp bằng tên đầy đủ (ví dụ: System.Console.WriteLine()).
o	using directive (Câu lệnh using): Dùng từ khóa using để import namespace, giúp gọi tên ngắn gọn hơn.
using System; // Import toàn bộ System
using MyCompany.MyApp.Utilities; // Import namespace con
// ...
Console.WriteLine("Hello"); // Thay vì System.Console.WriteLine()
Helper myHelper = new Helper();

4.	 Controller, View trong .Net MVC
Trong .NET MVC, Controller là bộ điều khiển logic, xử lý yêu cầu người dùng, tương tác với Model (dữ liệu) và chọn View thích hợp để hiển thị kết quả, trong khi View là thành phần giao diện, nhiệm vụ hiển thị dữ liệu từ Model cho người dùng, không truy cập trực tiếp vào Model mà nhận dữ liệu đã được xử lý từ Controller. Chúng cùng nhau tạo thành một kiến trúc phân tách logic nghiệp vụ và giao diện, giúp ứng dụng dễ bảo trì. 
o	Controller
•	Vai trò: Cầu nối, điều phối giữa Model và View.
•	Nhiệm vụ:
o	Nhận yêu cầu HTTP từ người dùng.
o	Gọi Model để lấy hoặc xử lý dữ liệu (logic nghiệp vụ).
o	Chọn View phù hợp để trình bày dữ liệu (Model).
o	Trả về kết quả (thường là View đã được render) cho người dùng.
o	View
•	Vai trò: Giao diện người dùng (UI).
•	Nhiệm vụ:
o	Nhận dữ liệu từ Controller (thường dưới dạng ViewModel).
o	Hiển thị dữ liệu đó dưới dạng HTML, JSON, XML, v.v..
o	Chịu trách nhiệm về cách dữ liệu được trình bày, không chứa logic nghiệp vụ hoặc truy cập trực tiếp database.
o	Xử lý tương tác của người dùng và gửi lại yêu cầu (thường qua Controller).


