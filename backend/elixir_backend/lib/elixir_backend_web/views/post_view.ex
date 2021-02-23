defmodule ElixirBackendWeb.PostView do
  use ElixirBackendWeb, :view

  def render("foo.json",%{data: data}) do
    %{data: data}
  end
end
