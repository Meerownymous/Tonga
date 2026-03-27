// using System;
// using System.IO;
// using System.Net.Http;
// using System.Text;
// using System.Threading.Tasks;
// using Tonga.Bytes;
// using Tonga.Text;
//
// namespace Tonga.IO;
//
// /// <summary>
// /// Input out of other things.
// /// </summary>
// public sealed class AsConduit : IConduit, IDisposable
// {
//     /// <summary>
//     /// the input
//     /// </summary>
//     private readonly Lazy<Stream> origin;
//
//     public AsConduit(IConduit conduit) : this(conduit.Stream)
//     { }
//
//     /// <summary>
//     /// Input out of a file Uri.
//     /// </summary>
//     /// <param name="uri">uri of a file, get with Path.GetFullPath(relativePath) or prefix with file://...</param>
//     public AsConduit(Uri uri) : this(
//         () =>
//         {
//             Stream result;
//             if (uri.HostNameType == UriHostNameType.Dns)
//             {
//                 var stream = Task.Run(async () =>
//                 {
//                     using HttpClient client = new HttpClient();
//                     HttpResponseMessage response = await client.GetAsync(uri.AbsoluteUri);
//                     HttpContent content = response.Content;
//                     {
//                         return await content.ReadAsStreamAsync();
//                     }
//                 });
//                 result = stream.Result;
//             }
//             else
//                 result = new FileStream(Uri.UnescapeDataString(uri.LocalPath), FileMode.OpenOrCreate, FileAccess.ReadWrite);
//             return result;
//         })
//     { }
//
//     /// <summary>
//     /// Input out of a file Uri.
//     /// </summary>
//     /// <param name="file">uri of a file, get with Path.GetFullPath(relativePath) or prefix with file://...</param>
//     public AsConduit(FileInfo file) : this(() => file)
//     { }
//
//     /// <summary>
//     /// Input out of a scalar of a file Uri.
//     /// </summary>
//     /// <param name="file">scalar of a uri of a file, get with Path.GetFullPath(relativePath) or prefix with file://...</param>
//     public AsConduit(Func<FileInfo> file) : this(
//         () => new FileStream(Uri.UnescapeDataString(file().FullName), FileMode.Open, FileAccess.ReadWrite))
//     { }
//
//     /// <summary>
//     /// Input out of a Url.
//     /// </summary>
//     /// <param name="url">a url starting with http:// or https://</param>
//     public AsConduit(Url url) : this(() => url)
//     { }
//
//     /// <summary>
//     /// Input out of a Url scalar.
//     /// </summary>
//     /// <param name="url">a url starting with http:// or https://</param>
//     public AsConduit(Func<Url> url) : this(() =>
//         {
//             var stream = Task.Run(async () =>
//             {
//                 using HttpClient client = new HttpClient();
//                 HttpResponseMessage response = await client.GetAsync(url().Value());
//                 HttpContent content = response.Content;
//                 {
//                     return await content.ReadAsStreamAsync();
//                 }
//             });
//             return stream.Result;
//         })
//     { }
//
//     /// <summary>
//     /// ctor
//     /// </summary>
//     /// <param name="rdr">a streamreader</param>
//     public AsConduit(StreamReader rdr) : this(new BytesMorph(rdr))
//     { }
//
//     /// <summary>
//     /// ctor
//     /// </summary>
//     /// <param name="rdr">a streamreader</param>
//     /// <param name="enc">encoding of the reader</param>
//     public AsConduit(StreamReader rdr, Encoding enc) : this(new BytesMorph(rdr, enc))
//     { }
//
//     /// <summary>
//     /// ctor
//     /// </summary>
//     /// <param name="str">a stream</param>
//     /// <param name="enc">encoding of the stream</param>
//     public AsConduit(Stream str, Encoding enc) : this(new BytesMorph(new StreamReader(str), enc))
//     { }
//
//     /// <summary>
//     /// ctor
//     /// </summary>
//     /// <param name="rdr">a streamreader</param>
//     /// <param name="enc">encoding of the reader</param>
//     /// <param name="max">maximum buffer size</param>
//     public AsConduit(StreamReader rdr, Encoding enc, int max = 16 << 10) : this(new BytesMorph(rdr, enc, max))
//     { }
//
//     /// <summary>
//     /// ctor
//     /// </summary>
//     /// <param name="builder">a stringbuilder</param>
//     public AsConduit(StringBuilder builder) : this(builder, Encoding.UTF8)
//     { }
//
//     /// <summary>
//     /// ctor
//     /// </summary>
//     /// <param name="builder">a stringbuilder</param>
//     /// <param name="enc">encoding of the stringbuilder</param>
//     public AsConduit(StringBuilder builder, Encoding enc) : this(() =>
//         new MemoryStream(
//             new BytesMorph(builder, enc).Raw()
//         )
//     )
//     { }
//
//     /// <summary>
//     /// ctor
//     /// </summary>
//     /// <param name="chars">some chars</param>
//     public AsConduit(params char[] chars) : this(new BytesMorph(chars))
//     { }
//
//     /// <summary>
//     /// ctor
//     /// </summary>
//     /// <param name="chars">some chars</param>
//     /// <param name="enc">encoding of the chars</param>
//     public AsConduit(char[] chars, Encoding enc) : this(new BytesMorph(chars, enc))
//     { }
//
//     /// <summary>
//     /// ctor
//     /// </summary>
//     /// <param name="text">some text</param>
//     public AsConduit(String text) : this(new BytesMorph(text))
//     { }
//
//     /// <summary>
//     /// ctor
//     /// </summary>
//     /// <param name="text">some <see cref="string"/></param>
//     /// <param name="enc"><see cref="Encoding"/> of the string</param>
//     public AsConduit(String text, Encoding enc) : this(new BytesMorph(text, enc))
//     { }
//
//     /// <summary>
//     /// ctor
//     /// </summary>
//     /// <param name="text">some <see cref="IText"/></param>
//     public AsConduit(IText text) : this(new BytesMorph(text))
//     { }
//
//     /// <summary>
//     /// ctor
//     /// </summary>
//     /// <param name="text">some <see cref="IText"/></param>
//     /// <param name="encoding"><see cref="Encoding"/> of the text</param>
//     public AsConduit(IText text, Encoding encoding) : this(new BytesMorph(text, encoding))
//     { }
//
//     /// <summary>
//     /// ctor
//     /// </summary>
//     /// <param name="error"><see cref="Exception"/> to serialize</param>
//     public AsConduit(Exception error) : this(new BytesMorph(error))
//     { }
//
//     /// <summary>
//     /// ctor
//     /// </summary>
//     /// <param name="bytes">a <see cref="byte"/> array</param>
//     public AsConduit(byte[] bytes) : this(new BytesMorph(bytes))
//     { }
//
//     /// <summary>
//     /// ctor
//     /// </summary>
//     /// <param name="src">a <see cref="IBytes"/> object which will be copied to memory</param>
//     public AsConduit(IBytes src) : this(
//         () =>
//         {
//             var b = src.Raw();
//             var m = new MemoryStream();
//             m.Write(b, 0, b.Length);
//             m.Seek(0, SeekOrigin.Begin);
//             return m;
//         }
//     )
//     { }
//
//     /// <summary>
//     /// ctor
//     /// </summary>
//     /// <param name="stream">a <see cref="Stream"/> as input</param>
//     public AsConduit(Stream stream) : this(() => stream)
//     { }
//
//     /// <summary>
//     /// ctor
//     /// </summary>
//     /// <param name="stream">the input <see cref="Stream"/></param>
//     public AsConduit(Func<Stream> stream)
//     {
//         this.origin = new Lazy<Stream>(stream);
//     }
//
//     /// <summary>
//     /// Get the stream.
//     /// </summary>
//     /// <returns>the stream</returns>
//     public Stream Stream() => origin.Value;
//
//     /// <summary>
//     /// Clean up.
//     /// </summary>
//     public void Dispose() => origin.Value.Dispose();
// }
