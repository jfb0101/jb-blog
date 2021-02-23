defmodule ElixirBackendWeb.PostController do
  use ElixirBackendWeb, :controller

  def get_post(conn,params) do
    IO.inspect(params)
    render(conn,"foo.json",%{data: params[Atom.to_string(:post_id)]})
  end
end
