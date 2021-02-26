
defmodule ElixirBackend.UseCases.User.Get.Params do
  defstruct email: :nil, id: :nil
end

defmodule ElixirBackend.UseCases.User.Get do

  alias ElixirBackend.DB.Cassandra
  alias ElixirBackend.UseCases.User.Get.Params

  def call(%ElixirBackend.UseCases.User.Get.Params{} = params) do
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

    [query,params] = case params do
      %Params{id: id} when id != nil -> search[:by_id]
      %Params{email: email} when email != nil -> search[:by_email]
    end

    {:ok,prepared} = Xandra.prepare(conn,query)
    {:ok, %Xandra.Page{} = page} = Xandra.execute(conn,prepared,params)

    page
  end



end
