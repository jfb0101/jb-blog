defmodule ElixirBackend.UseCases.User.Create do
  alias ElixirBackend.Models.User
  alias ElixirBackend.Security
  def call(%User{} = user) do
    {:ok,cassandraConn} = ElixirBackend.DB.Cassandra.GetCassandraConnection.call()

    {:ok,prepared} = Xandra.prepare(cassandraConn,"insert into user (id,email,name,password) values (?,?,?,?)")

    {:ok,%Xandra.Void{}} = Xandra.execute(cassandraConn,prepared,[
      UUID.uuid4(),
      user.email,
      user.name,
      user.password |> Security.get_md5_hash()
    ])
  end


end
