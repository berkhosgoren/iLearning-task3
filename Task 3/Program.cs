using System.Numerics;

var builder = WebApplication.CreateBuilder(args);

// Swagger (for local testing)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

static bool TryParseNatural(string? s, out BigInteger value)
{
    value = BigInteger.Zero;

    if (string.IsNullOrWhiteSpace(s))
        return false;
    foreach (var ch in s)
    {
        if (ch < '0' || ch > '9')
            return false;
    }

    // BigInteger parse (for huge numbers)
    value = BigInteger.Parse(s);

    // Accept 0 as natural 
    return value >= 0;
}

static BigInteger Gcd(BigInteger a, BigInteger b)
{
    while (b != 0)
    {
        var t = a % b;
        a = b;
        b = t;
    }
    return BigInteger.Abs(a);
}

static BigInteger Lcm(BigInteger a, BigInteger b)
{
    if (a == 0 || b == 0)
        return 0;

    return BigInteger.Abs(a / Gcd(a, b) * b);
}

app.MapGet("/berkhosgoren1_gmail_com", (string? x, string? y) =>
{
    if (!TryParseNatural(x, out var a) || !TryParseNatural(y, out var b))
        return Results.Text("NaN", "text/plain");

    var l = Lcm(a, b);
    return Results.Text(l.ToString(), "text/plain");
});

app.Run();