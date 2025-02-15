-- Bảng users
CREATE TABLE users
(
    id            SERIAL PRIMARY KEY,
    full_name     VARCHAR(100)        NOT NULL,
    email         VARCHAR(255) UNIQUE NOT NULL,
    password_hash TEXT                NOT NULL,
    role          VARCHAR(50) DEFAULT 'user',
    last_login    TIMESTAMP,
    created_at    TIMESTAMP   DEFAULT NOW()
);

-- Bảng categories
CREATE TABLE categories
(
    id      SERIAL PRIMARY KEY,
    user_id INT          NOT NULL REFERENCES users (id) ON DELETE CASCADE,
    name    VARCHAR(100) NOT NULL,
    UNIQUE (user_id, name)
);

-- Bảng tasks (Dùng VARCHAR thay vì ENUM)
CREATE TABLE tasks
(
    id          SERIAL PRIMARY KEY,
    user_id     INT                      NOT NULL REFERENCES users (id) ON DELETE CASCADE,
    category_id INT                      REFERENCES categories (id) ON DELETE SET NULL,
    title       VARCHAR(255)             NOT NULL,
    description TEXT,
    priority    VARCHAR(10)              NOT NULL CHECK (priority IN ('Low', 'Medium', 'High')),
    due_date    TIMESTAMP WITH TIME ZONE NOT NULL,
    status      VARCHAR(20)              DEFAULT 'todo' CHECK (status IN ('todo', 'in_progress', 'done', 'archived')),
    created_at  TIMESTAMP WITH TIME ZONE DEFAULT NOW()
);

-- Bảng reminders
CREATE TABLE reminders
(
    id            SERIAL PRIMARY KEY,
    task_id       INT       NOT NULL REFERENCES tasks (id) ON DELETE CASCADE,
    reminder_time TIMESTAMP NOT NULL,
    is_sent       BOOLEAN DEFAULT FALSE,
    sent_at       TIMESTAMP
);

-- Tạo indexes
CREATE INDEX idx_tasks_user_id ON tasks (user_id);
CREATE INDEX idx_tasks_due_date ON tasks (due_date);
CREATE INDEX idx_tasks_status ON tasks (status);





