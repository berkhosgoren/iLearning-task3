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

static bool TryParseNatural(string? s, out long value)
{
    value = 0;

    if (string.IsNullOrWhiteSpace(s))
        return false;

    long v = 0;
    foreach (var ch in s)
    {
        if (ch < '0' || ch > '9')
            return false;
        try
        {
            checked { v = v * 10 + (ch - '0'); }
        }
        catch (OverflowException)
        {
            return false;
        }
    }

    if (v <= 0)
        return false;

    value = v;
    return true;
}

static long Gcd(long a, long b)
{
    while (b != 0)
    {
        var t = a % b;
        a = b;
        b = t;
    }
    return a;
}

static bool TryLcm(long a, long b, out long lcm)
{
    lcm = 0;

    var g = Gcd(a, b);
    var part = a / g;

    try
    {
        checked { lcm = part * b; }
        return true;
    }
    catch (OverflowException)
    {
        return false;
    }
}

app.MapGet("/berkhosgoren1_gmail_com", (string? x, string? y) =>
{
    if (!TryParseNatural(x, out var a) || !TryParseNatural(y, out var b))
        return Results.Text("NaN", "text/plain");

    if (!TryLcm(a, b, out var l))
        return Results.Text("NaN", "text/plain");

    return Results.Text(l.ToString(), "text/plain");
});

app.Run();
