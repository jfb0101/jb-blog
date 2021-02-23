namespace FsharpBackend.DB.RowToObj
open Cassandra
open FsharpBackend.Models

module UserRowToObj =
    let ``$`` (row: Row) : User option = 
        match row.Length with
            | 0 -> None
            | _ -> Some(
                    {Id = row.GetValue("id");
                    Name = row.GetValue("name");
                    Email = row.GetValue("email");
                    Password = try 
                                    row.GetValue("password")
                                with 
                                    | _ -> ""})