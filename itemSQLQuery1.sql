SELECT 
	categories.category_id AS CatId,
	categories.category_title AS ChannelName,
	categories.category_description AS ChannelDescription
FROM categories
WHERE category_id = 2
SELECT TOP(5)
	news.news_id AS ItemId, 
	news.news_title AS ItemTitle, 
	news.news_content AS ItemContent, 
	news.news_postdate AS PubDate, 
	users.user_name AS Author
FROM categories 
INNER JOIN news 
	ON news.fk_categories_id = categories.category_id
INNER JOIN users 
	ON users.user_id = news.fk_users_id 
WHERE category_id = 2 
ORDER BY PubDate DESC