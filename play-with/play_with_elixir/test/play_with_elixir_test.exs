defmodule PlayWithElixirTest do
  use ExUnit.Case
  doctest PlayWithElixir

  test "greets the world" do
    assert PlayWithElixir.hello() == :world
  end
end
