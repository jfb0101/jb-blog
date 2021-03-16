defmodule ElixirBackend.Test.Integration.UseCases.User.Get do
  use ExUnit.Case

  test "get user", %{} do
    result = ElixirBackend.UseCases.User.Get.call(%ElixirBackend.UseCases.User.Get.Params{email: "jfb0101@gmail.com"})

    IO.inspect(result)
  end
end
