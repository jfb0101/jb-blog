defmodule ElixirBackend.Test.Integration.UseCases.User.GetAll do
  use ExUnit.Case

  alias ElixirBackend.UseCases.User

  test "get all users", %{} do
    result = User.GetAll.call()

    assert length(result) == 3
  end
end
