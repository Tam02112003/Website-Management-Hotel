<div class="dark:[&amp;_pre:has(code)]:bg-shade dark:[&amp;_code[data-lang]]:bg-shade relative flex flex-col gap-1 rounded-md border-[0.5px] border-[#d0d0d0] bg-[#f8f8f8] p-4 text-white [overflow-wrap:anywhere] dark:border-[#2F2F2F] dark:bg-[#141414] [&amp;_pre]:my-3"><div class="prose-custom prose-custom-md prose-custom-gray !max-w-none text-neutral-300 [overflow-wrap:anywhere]"><p>I'll create a README.md file for the Website-Management-Hotel repository based on the codebase information provided.</p>
<h1 class="group flex items-center">Hotel Management System  </h1>
<p align="center">  
  <img alt="Bootstrap logo" width="200" height="165" src="https://getbootstrap.com/docs/5.2/assets/brand/bootstrap-logo-shadow.png">  
</p>  
<h2 class="group flex items-center">Overview  </h2>
<p>This is a comprehensive hotel management web application built with ASP.NET Core 7.0. The system provides functionality for managing hotel rooms, bookings, services, and user accounts with different access levels (admin and regular users).</p>
<h2 class="group flex items-center">Technologies Used  </h2>
<ul>
<li><strong>ASP.NET Core 7.0</strong> - Web application framework</li>
<li><strong>Entity Framework Core</strong> - ORM for database operations</li>
<li><strong>SQL Server</strong> - Database</li>
<li><strong>ASP.NET Identity</strong> - Authentication and authorization</li>
<li><strong>Bootstrap 5</strong> - Frontend styling</li>
<li><strong>jQuery</strong> - JavaScript library for DOM manipulation</li>
<li><strong>ApexCharts</strong> - Data visualization for admin dashboard</li>
</ul>
<h2 class="group flex items-center">Features  </h2>
<h3 class="group flex items-center">User Features  </h3>
<ul>
<li>Browse available rooms</li>
<li>Create and manage bookings</li>
<li>View booking history</li>
</ul>
<h3 class="group flex items-center">Admin Features  </h3>
<ul>
<li>Manage room types (LoaiPhong) and rooms (Phong)</li>
<li>Process and approve booking requests</li>
<li>Manage services</li>
<li>Monitor bookings and reservations</li>
<li>Content management for news/announcements</li>
</ul>
<h2 class="group flex items-center">Project Structure  </h2>
<pre class="px-2 py-1.5 has-[code]:rounded-md has-[code]:!bg-[#e5e5e5] has-[div]:bg-transparent has-[div]:!p-0 has-[code]:text-stone-900 dark:has-[code]:!bg-[#242424] has-[code]:dark:text-white [&amp;_code]:block [&amp;_code]:border-none [&amp;_code]:bg-transparent [&amp;_code]:p-0"><code class="rounded-sm bg-[#e5e5e5] px-[0.25rem] py-[0.20rem] text-xs font-normal leading-[15px] before:hidden after:hidden dark:bg-[#484848]/30">DACS_QLKS/  
├── Areas/  
│   ├── Admin/               # Admin area for hotel management  
│   │   └── Controllers/     # Admin controllers for various management functions  
│   ├── Identity/            # User authentication and account management  
│   └── User/                # User area for booking and general usage  
│       └── Controllers/     # User controllers for booking and home  
├── Data/                    # Database context and migrations  
├── Models/                  # Domain models  
├── Repository/              # Repository pattern implementation  
├── Views/                   # Shared views  
└── wwwroot/                 # Static files (CSS, JS, images)  
    └── assets/  
        └── libs/            # Frontend libraries (Bootstrap, jQuery, ApexCharts)  
</code></pre>
<h2 class="group flex items-center">Core Models  </h2>
<p>The application uses several key models including:</p>
<ul>
<li><strong>Booking</strong>: Manages reservation data including customer information, check-in/out dates, room details, and pricing</li>
<li><strong>Phong</strong>: Represents hotel rooms</li>
<li><strong>LoaiPhong</strong>: Represents room types/categories</li>
<li><strong>DichVu</strong>: Represents hotel services</li>
<li><strong>ServiceOrder</strong>: Tracks services ordered with bookings</li>
</ul>
<h2 class="group flex items-center">Repository Pattern  </h2>
<p>The application uses the repository pattern for data access, providing a clean separation between data access logic and business logic. Example implementation:</p>
<pre class="px-2 py-1.5 has-[code]:rounded-md has-[code]:!bg-[#e5e5e5] has-[div]:bg-transparent has-[div]:!p-0 has-[code]:text-stone-900 dark:has-[code]:!bg-[#242424] has-[code]:dark:text-white [&amp;_code]:block [&amp;_code]:border-none [&amp;_code]:bg-transparent [&amp;_code]:p-0"><pre style="display: block; overflow-x: auto; padding: 6px 8px; color: rgb(51, 51, 51); background: transparent; margin: 0px; width: 100%;"><code style="white-space: pre; font-size: 12px;"><span style="color: rgb(51, 51, 51); font-weight: bold;">public</span><span> </span><span style="color: rgb(51, 51, 51); font-weight: bold;">class</span><span> </span><span style="color: rgb(153, 0, 0); font-weight: bold;">LoaiPhongRepository</span><span>: </span><span style="color: rgb(153, 0, 0); font-weight: bold;">ILoaiPhongRepository</span><span>  
</span>{  
<span>    </span><span style="color: rgb(51, 51, 51); font-weight: bold;">private</span><span> </span><span style="color: rgb(51, 51, 51); font-weight: bold;">readonly</span><span> ApplicationDbContext _db;  
</span>      
<span>    </span><span class="hljs-function" style="color: rgb(51, 51, 51); font-weight: bold;">public</span><span class="hljs-function"> </span><span class="hljs-function" style="color: rgb(153, 0, 0); font-weight: bold;">LoaiPhongRepository</span><span class="hljs-function">(</span><span class="hljs-function hljs-params">ApplicationDbContext db</span><span class="hljs-function">)</span><span>  
</span>    {  
        _db = db;  
    }  
      
<span>    </span><span class="hljs-function" style="color: rgb(51, 51, 51); font-weight: bold;">public</span><span class="hljs-function"> IEnumerable&lt;LoaiPhong&gt; </span><span class="hljs-function" style="color: rgb(153, 0, 0); font-weight: bold;">GetAll</span><span class="hljs-function">(</span><span class="hljs-function">)</span><span>  
</span>    {  
<span>        </span><span style="color: rgb(51, 51, 51); font-weight: bold;">return</span><span> _db.LoaiPhongs.ToList();  
</span>    }  
      
<span>    </span><span class="hljs-function" style="color: rgb(51, 51, 51); font-weight: bold;">public</span><span class="hljs-function"> LoaiPhong </span><span class="hljs-function" style="color: rgb(153, 0, 0); font-weight: bold;">GetByID</span><span class="hljs-function">(</span><span class="hljs-function hljs-params" style="color: rgb(0, 134, 179);">int</span><span class="hljs-function hljs-params"> id</span><span class="hljs-function">)</span><span>  
</span>    {  
<span>        </span><span style="color: rgb(51, 51, 51); font-weight: bold;">return</span><span> _db.LoaiPhongs.Find(id);  
</span>    }  
      
<span>    </span><span style="color: rgb(153, 153, 136); font-style: italic;">// Additional CRUD operations...  </span><span>
</span>}</code></pre></pre>
<h2 class="group flex items-center">Setup and Installation  </h2>
<h3 class="group flex items-center">Prerequisites  </h3>
<ul>
<li>.NET 7.0 SDK</li>
<li>SQL Server</li>
<li>Visual Studio 2022 (recommended)</li>
</ul>
<h3 class="group flex items-center">Getting Started  </h3>
<ol>
<li>
<p>Clone the repository:</p>
<pre class="px-2 py-1.5 has-[code]:rounded-md has-[code]:!bg-[#e5e5e5] has-[div]:bg-transparent has-[div]:!p-0 has-[code]:text-stone-900 dark:has-[code]:!bg-[#242424] has-[code]:dark:text-white [&amp;_code]:block [&amp;_code]:border-none [&amp;_code]:bg-transparent [&amp;_code]:p-0"><code class="rounded-sm bg-[#e5e5e5] px-[0.25rem] py-[0.20rem] text-xs font-normal leading-[15px] before:hidden after:hidden dark:bg-[#484848]/30">git clone https://github.com/Tam02112003/Website-Management-Hotel.git  
</code></pre>
</li>
<li>
<p>Open the solution file in Visual Studio</p>
</li>
<li>
<p>Update the connection string in <code class="rounded-sm bg-[#e5e5e5] px-[0.25rem] py-[0.20rem] text-xs font-normal leading-[15px] before:hidden after:hidden dark:bg-[#484848]/30">appsettings.json</code></p>
</li>
<li>
<p>Apply database migrations:</p>
<pre class="px-2 py-1.5 has-[code]:rounded-md has-[code]:!bg-[#e5e5e5] has-[div]:bg-transparent has-[div]:!p-0 has-[code]:text-stone-900 dark:has-[code]:!bg-[#242424] has-[code]:dark:text-white [&amp;_code]:block [&amp;_code]:border-none [&amp;_code]:bg-transparent [&amp;_code]:p-0"><code class="rounded-sm bg-[#e5e5e5] px-[0.25rem] py-[0.20rem] text-xs font-normal leading-[15px] before:hidden after:hidden dark:bg-[#484848]/30">Update-Database  
</code></pre>
</li>
<li>
<p>Build and run the application</p>
</li>
</ol>
<h2 class="group flex items-center">Authentication  </h2>
<p>The application uses ASP.NET Identity for authentication and role-based authorization:</p>
<pre class="px-2 py-1.5 has-[code]:rounded-md has-[code]:!bg-[#e5e5e5] has-[div]:bg-transparent has-[div]:!p-0 has-[code]:text-stone-900 dark:has-[code]:!bg-[#242424] has-[code]:dark:text-white [&amp;_code]:block [&amp;_code]:border-none [&amp;_code]:bg-transparent [&amp;_code]:p-0"><pre style="display: block; overflow-x: auto; padding: 6px 8px; color: rgb(51, 51, 51); background: transparent; margin: 0px; width: 100%;"><code style="white-space: pre; font-size: 12px;"><span>builder.Services.AddIdentity&lt;IdentityUser, IdentityRole&gt;(options =&gt;   
</span><span>    options.SignIn.RequireConfirmedAccount = </span><span style="color: rgb(0, 128, 128);">true</span><span>)  
</span>    .AddDefaultTokenProviders()  
    .AddDefaultUI()  
    .AddEntityFrameworkStores&lt;ApplicationDbContext&gt;();</code></pre></pre>
<h2 class="group flex items-center">Frontend Libraries  </h2>
<p>The application uses several frontend libraries:</p>
<ul>
<li><strong>Bootstrap 5</strong>: For responsive UI components and layout</li>
<li><strong>jQuery</strong>: For DOM manipulation and AJAX requests</li>
<li><strong>ApexCharts</strong>: For data visualization in the admin dashboard</li>
</ul>
<h2 class="group flex items-center">Development  </h2>
<p>For development guidelines and more detailed information, please refer to the <a href="wiki/Developer-Guide">Developer Guide</a>.</p>
<h2 class="group flex items-center">License  </h2>
<p>This project is licensed under the MIT License - see the LICENSE file for details.</p>
<h2 class="group flex items-center">Contributors  </h2>
<ul>
<li>Nguyen Minh Tam</li>
</ul>
<p>Notes:<br>
This README file is based on the codebase information provided, focusing on the structure and technologies used in the Website-Management-Hotel repository. The file includes information about the project's architecture, key features, and setup instructions. I've included references to the Bootstrap and other frontend libraries that are used in the project, as shown in the static web assets files.</p>
<p>Wiki pages you might want to explore:</p>
<ul>
<li><a href="/Tam02112003/Website-Management-Hotel/5-developer-guide">Developer Guide (Tam02112003/Website-Management-Hotel)</a></li>
</ul></div><div class="flex items-center gap-1 text-neutral-300"><button class="rounded-lg p-1.5 transition-colors hover:bg-white/10 hover:text-white"><svg xmlns="http://www.w3.org/2000/svg" width="1em" height="1em" fill="currentColor" viewBox="0 0 256 256" class="h-4 w-4"><path d="M234,80.12A24,24,0,0,0,216,72H160V56a40,40,0,0,0-40-40,8,8,0,0,0-7.16,4.42L75.06,96H32a16,16,0,0,0-16,16v88a16,16,0,0,0,16,16H204a24,24,0,0,0,23.82-21l12-96A24,24,0,0,0,234,80.12ZM32,112H72v88H32ZM223.94,97l-12,96a8,8,0,0,1-7.94,7H88V105.89l36.71-73.43A24,24,0,0,1,144,56V80a8,8,0,0,0,8,8h64a8,8,0,0,1,7.94,9Z"></path></svg></button><button class="rounded-lg p-1.5 transition-colors hover:bg-white/10 hover:text-white"><svg xmlns="http://www.w3.org/2000/svg" width="1em" height="1em" fill="currentColor" viewBox="0 0 256 256" class="h-4 w-4"><path d="M239.82,157l-12-96A24,24,0,0,0,204,40H32A16,16,0,0,0,16,56v88a16,16,0,0,0,16,16H75.06l37.78,75.58A8,8,0,0,0,120,240a40,40,0,0,0,40-40V184h56a24,24,0,0,0,23.82-27ZM72,144H32V56H72Zm150,21.29a7.88,7.88,0,0,1-6,2.71H152a8,8,0,0,0-8,8v24a24,24,0,0,1-19.29,23.54L88,150.11V56H204a8,8,0,0,1,7.94,7l12,96A7.87,7.87,0,0,1,222,165.29Z"></path></svg></button></div></div>
