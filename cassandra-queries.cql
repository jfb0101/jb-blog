use jb_blog;

-- describe table post;

-- drop table post;

CREATE TABLE post (
    id text,
    created_on timestamp,
    body text,
    post_title text,
    title text,
    user_id text,
    user_name text,
    PRIMARY KEY (id)
);