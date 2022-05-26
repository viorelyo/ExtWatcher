using System.Security.Cryptography;

try
{
    using var md5 = MD5.Create();

    using var readStream = File.OpenRead(args[0]);
    var fileHash = BitConverter.ToString(md5.ComputeHash(readStream)).Replace("-", "");

    Environment.Exit(0);
}
catch
{
    Environment.Exit(1);
}