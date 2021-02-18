use jb_blog;

create table post (
    post_id text,
    post_title text,
  
    user_id text,
    user_name text,

    created_on timestamp,

    primary key ((post_id),created_on)
);