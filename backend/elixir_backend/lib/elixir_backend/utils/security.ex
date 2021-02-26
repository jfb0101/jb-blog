defmodule ElixirBackend.Security do
  def get_md5_hash(string), do: :crypto.hash(:md5,string) |> Base.encode16()
end
