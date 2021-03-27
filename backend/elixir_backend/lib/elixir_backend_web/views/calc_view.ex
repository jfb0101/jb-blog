defmodule ElixirBackendWeb.CalcView do
  def render("sum.json",%{result: result, values: values}) do
    %{result: result, values: values}
  end
end
