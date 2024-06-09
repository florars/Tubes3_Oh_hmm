-- MariaDB dump 10.19-11.4.1-MariaDB, for Win64 (AMD64)
--
-- Host: localhost    Database: tubes3_stima24
-- ------------------------------------------------------
-- Server version	11.4.1-MariaDB

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*M!100616 SET @OLD_NOTE_VERBOSITY=@@NOTE_VERBOSITY, NOTE_VERBOSITY=0 */;

--
-- Table structure for table `biodata`
--

DROP TABLE IF EXISTS `biodata`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `biodata` (
  `NIK` varchar(16) NOT NULL,
  `nama` varchar(100) DEFAULT NULL,
  `tempat_lahir` varchar(50) DEFAULT NULL,
  `tanggal_lahir` date DEFAULT NULL,
  `jenis_kelamin` enum('Laki-Laki','Perempuan') DEFAULT NULL,
  `golongan_darah` varchar(5) DEFAULT NULL,
  `alamat` varchar(255) DEFAULT NULL,
  `agama` varchar(50) DEFAULT NULL,
  `status_perkawinan` enum('Belum Menikah','Menikah','Cerai') DEFAULT NULL,
  `pekerjaan` varchar(100) DEFAULT NULL,
  `kewarganegaraan` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`NIK`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `biodata`
--

LOCK TABLES `biodata` WRITE;
/*!40000 ALTER TABLE `biodata` DISABLE KEYS */;
INSERT INTO `biodata` VALUES
('0468618366719292','T0r','Sumberejo','2005-02-11','Laki-Laki','O','Suite 23','Kristen','Cerai','Budget/Accounting Analyst I','Indonesia'),
('0711820263430417','Vcky','K├╢ln','2002-02-12','Perempuan','AB','Apt 859','Budhha','Belum Menikah','Programmer Analyst I','Germany'),
('1540007940331382','4nt0n1','Kan','2005-03-26','Laki-Laki','A','Room 1340','Islam','Menikah','Recruiting Manager','China'),
('1847603925975230','D10ny$u$','Ca├▒azas','2004-10-17','Perempuan','A','Apt 957','Budhha','Cerai','Graphic Designer','Panama'),
('2485357390713320','C4554ndr4','Karlstad','2005-01-27','Perempuan','O','PO Box 41557','Konghucu','Cerai','Assistant Media Planner','Sweden'),
('3010727334179471','M1ll1snt','Butterworth','2008-12-18','Laki-Laki','O','Room 82','Katolik','Belum Menikah','Desktop Support Technician','Malaysia'),
('3560334425313359','Ki1nny','Zhamao','2004-05-01','Laki-Laki','AB','5th Floor','Islam','Cerai','Account Coordinator','China'),
('3913467690979320','Jenl33','Vila Fria','2007-06-21','Laki-Laki','A','Room 1289','Budhha','Belum Menikah','Structural Analysis Engineer','Portugal'),
('4128008520273553','Th41n3','Ouro Branco','2000-06-27','Perempuan','A','PO Box 5762','Hindu','Belum Menikah','Librarian','Brazil'),
('8259657325083752','Dryn','Nasielsk','2004-07-11','Laki-Laki','AB','PO Box 11646','Hindu','Menikah','Computer Systems Analyst II','Poland'),
('8860808227235048','D0u9','Vila Ch├ú','2000-06-05','Laki-Laki','O','Apt 1840','Kristen','Belum Menikah','VP Product Management','Portugal'),
('9118731936305618','R3b3cc4','Pravdinsk','2007-06-11','Laki-Laki','A','Apt 586','Katolik','Cerai','Research Assistant IV','Russia');
/*!40000 ALTER TABLE `biodata` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `sidik_jari`
--

DROP TABLE IF EXISTS `sidik_jari`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `sidik_jari` (
  `berkas_citra` text DEFAULT NULL,
  `nama` varchar(100) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sidik_jari`
--

LOCK TABLES `sidik_jari` WRITE;
/*!40000 ALTER TABLE `sidik_jari` DISABLE KEYS */;
INSERT INTO `sidik_jari` VALUES
('test\\image\\real\\1__M_Left_index_finger.BMP','Rebecca'),
('test\\image\\real\\2__F_Left_index_finger.BMP','Thaine'),
('test\\image\\real\\3__M_Left_index_finger.BMP','Vicky'),
('test\\image\\real\\4__M_Left_index_finger.BMP','Jeniee'),
('test\\image\\real\\5__M_Left_index_finger.BMP','Dionysus'),
('test\\image\\real\\6__M_Left_index_finger.BMP','Kiinny'),
('test\\image\\real\\7__M_Left_index_finger.BMP','Millisent'),
('test\\image\\real\\8__M_Left_index_finger.BMP','Doug'),
('test\\image\\real\\9__M_Left_index_finger.BMP','Antoni'),
('test\\image\\real\\10__M_Left_index_finger.BMP','Tore'),
('test\\image\\real\\11__M_Left_index_finger.BMP','Dryn'),
('test\\image\\real\\12__M_Left_index_finger.BMP','Cassandra');
/*!40000 ALTER TABLE `sidik_jari` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*M!100616 SET NOTE_VERBOSITY=@OLD_NOTE_VERBOSITY */;

-- Dump completed on 2024-06-09 18:30:41
