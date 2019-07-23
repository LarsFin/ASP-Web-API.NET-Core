variable "key_name" {}
variable "owner" {}
variable "aws_access_key" {}
variable "aws_secret_key" {}
variable "aws_security_group" {}

provider "aws" {
  profile = "default"
  access_key = "${var.aws_access_key}"
  secret_key = "${var.aws_secret_key}"
  region = "eu-west-1"
}

resource "aws_security_group" "peeps_security_group" {
  name = "${var.aws_security_group}"

  ingress {
    from_port = 0
    to_port = 9292
    protocol = "tcp"
    cidr_blocks = ["212.250.145.34/32"]
  }

  egress {
    from_port = 0
    to_port = 65535
    protocol = "tcp"
    cidr_blocks = ["0.0.0.0/0"]
  }

  tags = {
    Name = "${var.aws_security_group}"
  }
}

resource "aws_instance" "peeps" {
  ami = "ami-04facb3ed127a2eb6"
  instance_type = "t2.micro"
  vpc_security_group_ids = ["${aws_security_group.peeps_security_group.id}"]
  key_name = "${var.key_name}"
  tags = {
    Owner = "${var.owner}"
    Name = "Peeps"
    Role = "Serving the People"
    "24x7" = "NO"
  }

  provisioner "file" {
    source = "docker-compose.yml"
    destination = "/tmp/docker-compose.yml"
    connection {
      type = "ssh"
      host = "${self.public_ip}"
      user = "ec2-user"
      private_key = "${file("~/.ssh/${var.key_name}.pem")}"
    }
  }
}
