defmodule ElixirBackend.DB.Cassandra.GetCassandraConnection do

  import Xandra


  def call() do
    after_connect_fn = fn conn ->
      Xandra.execute!(conn,"USE jb_blog")
    end

    {:ok,conn} = start_link(nodes: ["127.0.0.1:9042"], after_connect: after_connect_fn)

    conn
  end



end
