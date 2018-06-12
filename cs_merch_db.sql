-- MySQL dump 10.13  Distrib 5.7.17, for Win64 (x86_64)
--
-- Host: localhost    Database: cs_merchandise
-- ------------------------------------------------------
-- Server version	5.7.14

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `customer`
--

DROP TABLE IF EXISTS `customer`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `customer` (
  `customer_id` int(11) NOT NULL AUTO_INCREMENT,
  `firstname` varchar(45) DEFAULT NULL,
  `lastname` varchar(45) DEFAULT NULL,
  `contact` varchar(255) DEFAULT NULL,
  `cluster` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`customer_id`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `customer`
--

LOCK TABLES `customer` WRITE;
/*!40000 ALTER TABLE `customer` DISABLE KEYS */;
INSERT INTO `customer` VALUES (1,'Jam','Maligad','09454122474',NULL),(2,'Jan','Ko','911',NULL),(3,'Rj','Perias','101',NULL),(4,'asdd','dsa','1234567890',NULL),(5,'assda','asddas','121221212',NULL),(6,'Test','Test','123213123',''),(7,'Test1','Test2','1232134214',''),(8,'test3','test3','12312421','CS'),(9,'dsa','sad','2133213213','HUMLET'),(10,'Father','Denny','123','CS'),(11,'Test3','Test6','09090909','Accountancy');
/*!40000 ALTER TABLE `customer` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `merchandise`
--

DROP TABLE IF EXISTS `merchandise`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `merchandise` (
  `merch_id` int(11) NOT NULL AUTO_INCREMENT,
  `merch_name` varchar(45) DEFAULT NULL COMMENT 'with indicated Size',
  `merch_price` decimal(15,2) DEFAULT NULL,
  PRIMARY KEY (`merch_id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `merchandise`
--

LOCK TABLES `merchandise` WRITE;
/*!40000 ALTER TABLE `merchandise` DISABLE KEYS */;
INSERT INTO `merchandise` VALUES (1,'CS Shirt XS',250.00),(2,'CS Shirt S',350.00),(3,'CS Shirt M',450.00),(4,'CS Shirt L',550.00),(5,'CS Shirt XL',650.00);
/*!40000 ALTER TABLE `merchandise` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `order_claim`
--

DROP TABLE IF EXISTS `order_claim`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `order_claim` (
  `claim_id` int(11) NOT NULL AUTO_INCREMENT,
  `orderline_id` int(11) DEFAULT NULL,
  `quantity_no` int(11) DEFAULT NULL,
  `date_claimed` date DEFAULT NULL,
  PRIMARY KEY (`claim_id`),
  KEY `orderline_id_idx` (`orderline_id`),
  CONSTRAINT `orderline_id` FOREIGN KEY (`orderline_id`) REFERENCES `orderline` (`orderline_id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=28 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `order_claim`
--

LOCK TABLES `order_claim` WRITE;
/*!40000 ALTER TABLE `order_claim` DISABLE KEYS */;
INSERT INTO `order_claim` VALUES (1,15,0,'2018-06-11'),(2,16,1,'2018-06-11'),(3,17,0,'2018-06-11'),(4,12,1,'2018-06-11'),(5,13,1,'2018-06-11'),(6,14,1,'2018-06-11'),(7,18,1,'2018-06-12'),(8,15,0,'2018-06-12'),(9,18,0,'2018-06-12'),(10,18,0,'2018-06-12'),(11,12,0,'2018-06-12'),(12,19,1,'2018-06-12'),(13,19,2,'2018-06-12'),(14,19,2,'2018-06-12'),(15,20,4,'2018-06-12'),(16,20,1,'2018-06-12'),(17,21,2,'2018-06-12'),(18,21,1,'2018-06-12'),(19,21,1,'2018-06-12'),(20,21,4,'2018-06-12'),(21,21,2,'2018-06-12'),(22,21,0,'2018-06-12'),(23,21,0,'2018-06-12'),(24,12,0,'2018-06-12'),(25,12,0,'2018-06-12'),(26,12,0,'2018-06-12'),(27,22,10,'2018-06-12');
/*!40000 ALTER TABLE `order_claim` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `order_payment`
--

DROP TABLE IF EXISTS `order_payment`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `order_payment` (
  `payment_id` int(11) NOT NULL AUTO_INCREMENT,
  `order_id` int(11) DEFAULT NULL,
  `payment` decimal(10,2) DEFAULT NULL,
  `payment_date` date DEFAULT NULL,
  PRIMARY KEY (`payment_id`),
  KEY `order_id_idx` (`order_id`),
  CONSTRAINT `o_id` FOREIGN KEY (`order_id`) REFERENCES `orders` (`order_id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `order_payment`
--

LOCK TABLES `order_payment` WRITE;
/*!40000 ALTER TABLE `order_payment` DISABLE KEYS */;
INSERT INTO `order_payment` VALUES (1,13,700.00,'2018-06-11'),(2,14,1100.00,'2018-06-11'),(3,15,1200.00,'2018-06-11'),(4,16,3200.00,'2018-06-11'),(5,17,300.00,'2018-06-11'),(6,17,350.00,'2018-06-11'),(7,17,650.00,'2018-06-12'),(8,18,600.00,'2018-06-12'),(9,19,1750.00,'2018-06-12'),(10,20,6000.00,'2018-06-12'),(11,21,2323.00,'2018-06-12'),(12,22,123.00,'2018-06-12'),(13,23,1234.00,'2018-06-12');
/*!40000 ALTER TABLE `order_payment` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `orderline`
--

DROP TABLE IF EXISTS `orderline`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `orderline` (
  `orderline_id` int(11) NOT NULL AUTO_INCREMENT,
  `order_id` int(11) DEFAULT NULL,
  `merch_id` int(11) DEFAULT NULL,
  `quantity` int(11) DEFAULT NULL,
  `total_price` decimal(15,2) DEFAULT NULL,
  `quantity_claimed` int(11) DEFAULT NULL,
  PRIMARY KEY (`orderline_id`),
  KEY `merch_id_idx` (`merch_id`),
  KEY `order_id_idx` (`order_id`),
  CONSTRAINT `merch_id` FOREIGN KEY (`merch_id`) REFERENCES `merchandise` (`merch_id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `order_id` FOREIGN KEY (`order_id`) REFERENCES `orders` (`order_id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=25 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `orderline`
--

LOCK TABLES `orderline` WRITE;
/*!40000 ALTER TABLE `orderline` DISABLE KEYS */;
INSERT INTO `orderline` VALUES (1,11,1,1,250.00,NULL),(2,11,2,1,350.00,NULL),(3,12,1,1,250.00,NULL),(4,12,2,1,350.00,NULL),(5,12,3,1,450.00,NULL),(6,12,4,1,550.00,NULL),(7,1,1,1,250.00,NULL),(8,1,2,1,350.00,NULL),(9,2,1,1,250.00,0),(10,2,2,1,350.00,0),(11,2,3,1,450.00,0),(12,15,1,1,250.00,1),(13,15,2,1,350.00,1),(14,15,3,1,450.00,1),(15,16,1,1,250.00,1),(16,16,3,1,450.00,1),(17,16,5,1,650.00,0),(18,17,5,1,650.00,1),(19,18,1,5,1250.00,5),(20,19,2,5,1750.00,5),(21,20,4,10,5500.00,10),(22,21,4,10,5500.00,10),(24,23,1,11,2750.00,0);
/*!40000 ALTER TABLE `orderline` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `orders`
--

DROP TABLE IF EXISTS `orders`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `orders` (
  `order_id` int(11) NOT NULL AUTO_INCREMENT,
  `order_status` int(11) DEFAULT NULL,
  `customer_id` int(11) DEFAULT NULL,
  `payment_status` int(11) DEFAULT NULL,
  PRIMARY KEY (`order_id`),
  KEY `customer_id_idx` (`customer_id`),
  CONSTRAINT `customer_id` FOREIGN KEY (`customer_id`) REFERENCES `customer` (`customer_id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=24 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `orders`
--

LOCK TABLES `orders` WRITE;
/*!40000 ALTER TABLE `orders` DISABLE KEYS */;
INSERT INTO `orders` VALUES (1,1,2,NULL),(2,1,1,NULL),(3,1,1,NULL),(4,1,1,NULL),(5,1,1,NULL),(6,1,1,NULL),(7,1,1,NULL),(8,1,1,NULL),(9,1,1,NULL),(10,1,1,NULL),(11,1,1,NULL),(12,1,3,NULL),(13,1,1,NULL),(14,1,1,NULL),(15,1,1,NULL),(16,1,1,NULL),(17,1,1,NULL),(18,1,1,NULL),(19,1,1,NULL),(20,1,1,NULL),(21,1,1,NULL),(22,1,1,NULL),(23,1,1,NULL);
/*!40000 ALTER TABLE `orders` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `users` (
  `user_id` int(11) NOT NULL AUTO_INCREMENT,
  `username` varchar(45) DEFAULT NULL,
  `password` varchar(45) DEFAULT NULL,
  `firstname` varchar(45) DEFAULT NULL,
  `lastname` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`user_id`)
) ENGINE=MyISAM AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users`
--

LOCK TABLES `users` WRITE;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` VALUES (1,'jammaligad','123','Jam','Maligad');
/*!40000 ALTER TABLE `users` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2018-06-12 21:50:03
