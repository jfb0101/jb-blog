defmodule ElixirBackendWeb.PageController do
  use ElixirBackendWeb, :controller

  def index(conn, _params) do
    render(conn, "index.html")
  end

  def user(conn,_params) do
    user = %{name: "jonathan"}

    render(conn,"user.json",user: user)
  end

  def user2(_conn,_params) do
    user = %{name: "jonathan"}

    user
  end
end
