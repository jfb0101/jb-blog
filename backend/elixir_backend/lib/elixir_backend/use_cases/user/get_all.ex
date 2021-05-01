defmodule ElixirBackend.UseCases.User.GetAll do

  alias ElixirBackend.DB.Cassandra.GetCassandraConnection

  def call() do

    {:ok, prepared} = conn()
      |> Xandra.prepare("select * from user")

    {:ok, %Xandra.Page{} = page} = Xandra.execute(conn(), prepared)

    page
    |> Enum.to_list()
    
  end

  defp conn, do: GetCassandraConnection.getDbConn()
end
