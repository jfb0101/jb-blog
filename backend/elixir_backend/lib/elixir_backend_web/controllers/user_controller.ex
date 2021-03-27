defmodule ElixirBackendWeb.UserController do
    use ElixirBackendWeb, :controller

    alias ElixirBackend.UseCases.User.Login, as: UserLogin


    def login(conn,params) do
      %{"email" => email} = params

      user = UserLogin.call(email)

      render(conn,"user_login.json",%{user: user})
    end
end
