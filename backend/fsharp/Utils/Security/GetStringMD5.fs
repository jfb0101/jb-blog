namespace FsharpBackend.Utils.Security
open System.Text
open System.Security.Cryptography

module GetStringMD5 =
    let ``$`` (str:string) =
        let md5 = MD5.Create()

        let strBytes = UTF8Encoding().GetBytes(str)
        let hashBytes = md5.ComputeHash(strBytes)

        hashBytes
        |> Array.map (fun b -> System.String.Format("{0:X2}",b))
        |> String.concat System.String.Empty