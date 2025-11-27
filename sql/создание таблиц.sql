CREATE TABLE cinema.countries (
    id SERIAL PRIMARY KEY NOT NULL,
    name VARCHAR(100) NOT NULL UNIQUE
);

CREATE TABLE cinema.countries (
    id SERIAL PRIMARY KEY NOT NULL,
    name VARCHAR(100) NOT NULL UNIQUE
);

CREATE TABLE cinema.people (
    id SERIAL PRIMARY KEY NOT NULL,
    first_name VARCHAR(100) NOT NULL,
    last_name VARCHAR(100) NOT NULL,
    middle_name VARCHAR(100),
    birth_date DATE,
    profession VARCHAR(200),
    biography TEXT,
    studio_id INTEGER REFERENCES cinema.studios(id)
);

CREATE TABLE cinema.genres (
    id SERIAL PRIMARY KEY NOT NULL,
    name VARCHAR(50) NOT NULL UNIQUE,
    description TEXT
);

CREATE TABLE cinema.users (
    id SERIAL PRIMARY KEY NOT NULL,
    username VARCHAR(50) NOT NULL UNIQUE,
    email VARCHAR(255) NOT NULL UNIQUE,
    password_hash VARCHAR(255) NOT NULL,
    birth_date DATE,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE cinema.movies (
    id SERIAL PRIMARY KEY NOT NULL,
    title VARCHAR(255) NOT NULL,
    release_year INTEGER NOT NULL,
    description TEXT,
    duration INTEGER,
    type VARCHAR(50),
    country_id INTEGER REFERENCES cinema.countries(id),
    studio_id INTEGER REFERENCES cinema.studios(id),
    rating DECIMAL(3,1),
    age_rating VARCHAR(20)
);

CREATE TABLE cinema.collections (
    id SERIAL PRIMARY KEY,
    title VARCHAR(255) NOT NULL,
    description TEXT,
    user_id INTEGER NOT NULL REFERENCES cinema.users(id) ON DELETE CASCADE,
    is_public BOOLEAN DEFAULT TRUE,
	created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE cinema.movie_genres (
    movie_id INTEGER NOT NULL REFERENCES cinema.movies(id) ON DELETE CASCADE,
    genre_id INTEGER NOT NULL REFERENCES cinema.genres(id) ON DELETE CASCADE,
    PRIMARY KEY (movie_id, genre_id)
);

CREATE TABLE cinema.movie_people (
    movie_id INTEGER NOT NULL REFERENCES cinema.movies(id) ON DELETE CASCADE,
    person_id INTEGER NOT NULL REFERENCES cinema.people(id) ON DELETE CASCADE,
    role_type VARCHAR(100) NOT NULL,
    character_name VARCHAR(255),
    PRIMARY KEY (movie_id, person_id, role_type)
);

CREATE TABLE cinema.collection_movies (
    collection_id INTEGER NOT NULL REFERENCES cinema.collections(id) ON DELETE CASCADE,
    movie_id INTEGER NOT NULL REFERENCES cinema.movies(id) ON DELETE CASCADE,
    added_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (collection_id, movie_id)
);

CREATE TABLE cinema.person_countries (
    person_id INTEGER NOT NULL REFERENCES cinema.people(id) ON DELETE CASCADE,
    country_id INTEGER NOT NULL REFERENCES cinema.countries(id) ON DELETE CASCADE,
    PRIMARY KEY (person_id, country_id)
);