defmodule ElixirBackend.Test.Integration.UseCases.User.Get do
  use ExUnit.Case

  import ElixirBackend.UseCases.User.Get
  alias ElixirBackend.UseCases.User.Get.Input

  test "get user by email", %{} do
    result = call(%Input{email: "jfb0101@gmail.com"})

    IO.inspect(result)
  end

  test "get user by id", %{} do
    result = call(%Input{id: "384925d1-66d2-4e2c-be19-6201e91962ca"})

    IO.inspect(result)
  end
end
