defmodule ElixirBackendWeb.UserView do
  use ElixirBackendWeb, :view

  def render("user_login.json",data) do
    Map.get(data,:user)
  end
end
