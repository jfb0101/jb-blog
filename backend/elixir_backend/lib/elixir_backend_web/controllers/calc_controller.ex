defmodule ElixirBackendWeb.CalcController do
  use ElixirBackendWeb, :controller

  def sum(conn,_params) do
    %{query_params: query_params} = fetch_query_params(conn)

    values = query_params
    |> Map.take(["n1","n2"])
    |> Map.values()
    |> Enum.map(fn x -> String.to_integer(x) end)

    result = values
    |> Enum.sum()

    render(conn,"sum.json",%{result: result, values: values})
  end
end
