
defmodule ElixirBackend.UseCases.User.Get.Input do
  defstruct email: :nil, id: :nil
end

defmodule ElixirBackend.UseCases.User.Get do

  alias ElixirBackend.DB.Cassandra
  alias ElixirBackend.UseCases.User.Get.Input

  def call(%Input{} = params) do
    conn = Cassandra.GetCassandraConnection.call()

    search = [
      by_email: [
        "select * from user where email = ?",
        [params.email]],
      by_id: [
        "select * from user where id = ?",
        [params.id]
      ]
    ]

    [query,queryParams] = case params do
      %Input{id: id} when id != nil -> search[:by_id]
      %Input{email: email} when email != nil -> search[:by_email]
    end

    {:ok,prepared} = Xandra.prepare(conn,query)
    {:ok, %Xandra.Page{} = page} = Xandra.execute(conn,prepared,queryParams)

    page
    |> Enum.to_list()
    |> List.first()
    |> Map.drop(["password"])
  end



end
