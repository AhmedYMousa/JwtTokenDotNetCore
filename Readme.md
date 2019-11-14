# JWT Authentication using .Net Core 2.2

> In this repo I'm trying to demonstrate the steps to generate JWT token using ASP.NET Core 2.2.
## :memo: Where to start:

### Step 1: Understand JWT
-[x] Make share to visit [JWT](https://jwt.io/) to read the docs to understand what is JWT and how it's being constructed.

### Step 2: Startup.cs
-[x] Add the following code snippet to configure JWT tokens in your Startup.cs file.
>  services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt =>
            {
                opt.RequireHttpsMetadata = false;
                opt.SaveToken = false;
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });

### Step 3: Controller class
-[x] Implement method to generate token from your API endpoint, your method should including the following:
> var key = Encoding.UTF8.GetBytes(Configuration["AppSettings:JWT_Key"].ToString());
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new System.Security.Claims.ClaimsIdentity(new Claim[] {
                        new Claim("username",user.Username),
                        new Claim("role","Admin")
                    }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(key),
                         SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);

:::info
:bulb: **Hint:** It's better to use your `appSettings.json` file to store your secret JWT key or any settings your application requires.
:::

### Last:
Hope this demo help you to better understand how to implement JWT tokens in your application.