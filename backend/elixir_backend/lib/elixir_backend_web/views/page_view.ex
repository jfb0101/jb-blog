defmodule ElixirBackendWeb.PageView do
  use ElixirBackendWeb, :view

  def render("user.json",%{user: user}) do
    user
  end
end
