defmodule ElixirBackend.UseCases.User.Login do
  alias ElixirBackend.UseCases.User.Get, as: GetUser
  alias ElixirBackend.UseCases.User.Get.Input, as: GetUserInput

  def call(email) do
    GetUser.call(%GetUserInput{email: email})
  end
end
